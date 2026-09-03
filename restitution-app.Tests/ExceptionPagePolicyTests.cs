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
        [InlineData("Production")]
        public void AllowDeveloperExceptionPage_NonDevelopment_ReturnsFalse(string environmentName)
        {
            var result = ExceptionPagePolicy.AllowDeveloperExceptionPage(new FakeHostEnvironment(environmentName));

            Assert.False(result);
        }
    }
}
