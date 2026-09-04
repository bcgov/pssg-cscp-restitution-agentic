using Gov.Cscp.VictimServices.Public;
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
            Assert.Contains("MapHealthChecks(", programSource);
            Assert.Contains("\"/hc\"", programSource);
        }
    }
}
