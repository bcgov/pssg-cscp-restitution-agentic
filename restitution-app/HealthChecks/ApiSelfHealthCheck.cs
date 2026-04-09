using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Gov.Cscp.VictimServices.Public.HealthChecks
{
    /// <summary>
    /// Confirms the API process is running and able to serve requests.
    /// </summary>
    public class ApiSelfHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default
        )
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("API process is healthy.")
            );
        }
    }
}
