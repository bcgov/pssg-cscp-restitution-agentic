namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class CookiePolicyConfigurationTests
    {
        [Fact]
        public void MinimumSameSitePolicy_IsLax()
        {
            var programSource = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Program.cs"));

            Assert.Contains("MinimumSameSitePolicy = SameSiteMode.Lax", programSource);
            Assert.DoesNotContain("MinimumSameSitePolicy = SameSiteMode.None", programSource);
        }
    }
}
