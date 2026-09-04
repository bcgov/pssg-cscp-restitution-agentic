using Gov.Cscp.VictimServices.Public;
using Microsoft.Extensions.Hosting;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class ExceptionPagePolicyTests
    {
        private sealed class FakeHostEnvironment : IHostEnvironment
        {
            public FakeHostEnvironment(string environmentName) => EnvironmentName = environmentName;

            public string EnvironmentName { get; set; }
            public string ApplicationName { get; set; } = "restitution-app.Tests";
            public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
            public Microsoft.Extensions.FileProviders.IFileProvider ContentRootFileProvider { get; set; } =
                new Microsoft.Extensions.FileProviders.NullFileProvider();
        }

        [Fact]
        public void AllowDeveloperExceptionPage_Development_ReturnsTrue()
        {
            var result = ExceptionPagePolicy.AllowDeveloperExceptionPage(new FakeHostEnvironment("Development"));

            Assert.True(result);
        }

        [Theory]
        [InlineData("Staging")]
        [InlineData("Test")]
        [InlineData("QA")]
        [InlineData("UAT")]
        [InlineData("PreProduction")]
        [InlineData("Production")]
        public void AllowDeveloperExceptionPage_StagingLikeAndProduction_ReturnsFalse(string environmentName)
        {
            var result = ExceptionPagePolicy.AllowDeveloperExceptionPage(new FakeHostEnvironment(environmentName));

            Assert.False(result);
        }

        [Fact]
        public void Program_GatesDeveloperExceptionPageViaPolicy()
        {
            var programPath = Path.Combine(AppContext.BaseDirectory, "Program.cs");
            Assert.True(File.Exists(programPath), $"Expected linked Program.cs at {programPath}");
            var source = File.ReadAllText(programPath);
            Assert.Contains("ExceptionPagePolicy.AllowDeveloperExceptionPage", source);
            Assert.Contains("UseDeveloperExceptionPage", source);
            Assert.Contains("UseExceptionHandler", source);
        }
    }
}
