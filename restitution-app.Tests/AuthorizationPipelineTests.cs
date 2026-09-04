namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class AuthorizationPipelineTests
    {
        private static string ProgramSource => File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Program.cs"));

        [Fact]
        public void AuthorizationServices_AreRegistered()
        {
            Assert.Contains("services.AddAuthorization()", ProgramSource);
        }

        [Fact]
        public void UseAuthorization_RunsAfterRoutingAndBeforeControllerEndpoints()
        {
            var source = ProgramSource;

            var routing = source.IndexOf("app.UseRouting()", StringComparison.Ordinal);
            var authorization = source.IndexOf("app.UseAuthorization()", StringComparison.Ordinal);
            var controllers = source.IndexOf("app.MapControllers()", StringComparison.Ordinal);

            Assert.True(routing >= 0, "Program should call app.UseRouting().");
            Assert.True(authorization >= 0, "Program should call app.UseAuthorization().");
            Assert.True(controllers >= 0, "Program should call app.MapControllers().");
            Assert.True(routing < authorization, "app.UseAuthorization() must come after app.UseRouting().");
            Assert.True(authorization < controllers, "app.UseAuthorization() must come before app.MapControllers().");
        }

        [Fact]
        public void AnonymousHealthCheckEndpoint_DoesNotRequireAuthorization()
        {
            var source = ProgramSource;

            Assert.Contains("app.MapHealthChecks(", source);
            Assert.DoesNotContain("RequireAuthorization", source);
            Assert.DoesNotContain("FallbackPolicy", source);
        }
    }
}
