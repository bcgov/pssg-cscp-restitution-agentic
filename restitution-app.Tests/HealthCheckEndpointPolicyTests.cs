using System.Text.Json;
using Gov.Cscp.VictimServices.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class HealthCheckEndpointPolicyTests
    {
        private sealed class StubHealthCheck : IHealthCheck
        {
            public Task<HealthCheckResult> CheckHealthAsync(
                HealthCheckContext context,
                CancellationToken cancellationToken = default
            ) => Task.FromResult(HealthCheckResult.Healthy());
        }

        [Fact]
        public void IsAnonymousLivenessCheck_SelfTagged_ReturnsTrue()
        {
            var registration = new HealthCheckRegistration(
                "API",
                new StubHealthCheck(),
                failureStatus: null,
                tags: new[] { "self", "process" }
            );

            Assert.True(HealthCheckEndpointPolicy.IsAnonymousLivenessCheck(registration));
        }

        [Fact]
        public void IsAnonymousLivenessCheck_DataverseReadyTagged_ReturnsFalse()
        {
            var registration = new HealthCheckRegistration(
                "Dataverse",
                new StubHealthCheck(),
                failureStatus: null,
                tags: new[] { "dataverse", "dynamics", "ready" }
            );

            Assert.False(HealthCheckEndpointPolicy.IsAnonymousLivenessCheck(registration));
        }

        [Fact]
        public void ProgramMapsAnonymousHcWithSelfPredicate()
        {
            var programSource = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Program.cs"));

            Assert.Contains("Predicate = HealthCheckEndpointPolicy.IsAnonymousLivenessCheck", programSource);
            Assert.Contains("ResponseWriter = HealthCheckStatusResponseWriter.WriteStatusOnlyAsync", programSource);
            Assert.Contains("MapHealthChecks(", programSource);
            Assert.Contains("\"/hc\"", programSource);
            Assert.DoesNotContain("RequireAuthorization", programSource);
            Assert.DoesNotContain("checks = report.Entries", programSource);
            Assert.DoesNotContain("description = e.Value.Description", programSource);
        }

        [Fact]
        public async Task WriteStatusOnlyAsync_DoesNotSerializeCheckNamesOrDescriptions()
        {
            var report = new HealthReport(
                new Dictionary<string, HealthReportEntry>
                {
                    ["API"] = new(
                        HealthStatus.Healthy,
                        "API self check is healthy.",
                        TimeSpan.Zero,
                        exception: null,
                        data: null
                    ),
                    ["Dataverse"] = new(
                        HealthStatus.Unhealthy,
                        "Dataverse health check failed.",
                        TimeSpan.Zero,
                        exception: null,
                        data: null
                    ),
                },
                TimeSpan.Zero
            );
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await HealthCheckStatusResponseWriter.WriteStatusOnlyAsync(context, report);

            context.Response.Body.Position = 0;
            var body = await new StreamReader(context.Response.Body).ReadToEndAsync();
            using var json = JsonDocument.Parse(body);

            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal("Healthy", json.RootElement.GetProperty("status").GetString());
            Assert.Single(json.RootElement.EnumerateObject());
            Assert.DoesNotContain("API", body);
            Assert.DoesNotContain("Dataverse", body);
            Assert.DoesNotContain("API self check is healthy.", body);
            Assert.DoesNotContain("Dataverse health check failed.", body);
        }
    }
}
