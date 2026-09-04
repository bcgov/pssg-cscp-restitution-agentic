using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class AllowedHostsConfigurationTests
    {
        private static string GetFileContentPath(string relativePath)
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, relativePath);
                if (File.Exists(candidate))
                {
                    return candidate;
                }
                dir = dir.Parent;
            }
            throw new FileNotFoundException(
                $"Could not find file {relativePath} starting from {AppContext.BaseDirectory}"
            );
        }

        [Fact]
        public void AppSettings_AllowedHosts_IsNotWildcard()
        {
            var appsettingsJsonPath = GetFileContentPath("restitution-app/appsettings.json");
            var config = new ConfigurationBuilder().AddJsonFile(appsettingsJsonPath).Build();

            var allowedHosts = config["AllowedHosts"];

            Assert.NotNull(allowedHosts);
            Assert.NotEqual("*", allowedHosts);
            Assert.Contains("localhost", allowedHosts);
        }
    }
}
