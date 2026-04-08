using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Gov.Cscp.VictimServices.Public.HealthChecks
{
    /// <summary>
    /// Verifies that the application can communicate with Dynamics 365 / Dataverse.
    /// <list type="bullet">
    ///   <item>Checks <see cref="ServiceClient.IsReady"/> (in-memory, fast).</item>
    ///   <item>Executes a <see cref="WhoAmIRequest"/> to prove end-to-end connectivity
    ///         including authentication, network, and Dataverse availability.</item>
    /// </list>
    /// </summary>
    public class DataverseHealthCheck : IHealthCheck
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger<DataverseHealthCheck> _logger;

        /// <summary>
        /// Requests that take longer than this are reported as <see cref="HealthStatus.Degraded"/>.
        /// </summary>
        private static readonly TimeSpan DegradedThreshold = TimeSpan.FromSeconds(5);

        public DataverseHealthCheck(
            IOrganizationServiceAsync organizationService,
            ILogger<DataverseHealthCheck> logger
        )
        {
            _organizationService = organizationService;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default
        )
        {
            var data = new Dictionary<string, object>();

            try
            {
                // ── 1. Quick in-memory check ──────────────────────────────────
                if (_organizationService is ServiceClient serviceClient)
                {
                    if (!serviceClient.IsReady)
                    {
                        _logger.LogWarning(
                            "Dataverse health check: ServiceClient is not ready. LastError: {LastError}",
                            serviceClient.LastError
                        );

                        data["LastError"] = serviceClient.LastError ?? "Unknown";

                        return HealthCheckResult.Unhealthy(
                            "Dataverse ServiceClient is not ready.",
                            data: data
                        );
                    }
                }

                // ── 2. Round-trip connectivity check (WhoAmI) ─────────────────
                var stopwatch = Stopwatch.StartNew();

                var response = (WhoAmIResponse)
                    await _organizationService.ExecuteAsync(new WhoAmIRequest());

                stopwatch.Stop();
                var elapsed = stopwatch.Elapsed;

                data["ResponseTime"] = $"{elapsed.TotalMilliseconds:F0} ms";

                _logger.LogDebug(
                    "Dataverse health check succeeded in {ElapsedMs} ms (UserId={UserId})",
                    elapsed.TotalMilliseconds,
                    response.UserId
                );

                if (elapsed > DegradedThreshold)
                {
                    _logger.LogWarning(
                        "Dataverse health check response time ({ElapsedMs} ms) exceeds degraded threshold ({Threshold} ms)",
                        elapsed.TotalMilliseconds,
                        DegradedThreshold.TotalMilliseconds
                    );

                    return HealthCheckResult.Degraded(
                        $"Dataverse responded but took {elapsed.TotalMilliseconds:F0} ms (threshold: {DegradedThreshold.TotalMilliseconds:F0} ms).",
                        data: data
                    );
                }

                return HealthCheckResult.Healthy("Dataverse connection is healthy.", data: data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dataverse health check failed with exception");

                data["Exception"] = ex.Message;

                return HealthCheckResult.Unhealthy(
                    "Dataverse health check failed.",
                    exception: ex,
                    data: data
                );
            }
        }
    }
}
