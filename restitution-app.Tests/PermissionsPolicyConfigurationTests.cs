using System;
using System.IO;
using Xunit;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class PermissionsPolicyConfigurationTests
    {
        private static string GetFileContent(string relativePath)
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, relativePath);
                if (File.Exists(candidate))
                {
                    return File.ReadAllText(candidate);
                }
                dir = dir.Parent;
            }
            throw new FileNotFoundException(
                $"Could not find file {relativePath} starting from {AppContext.BaseDirectory}"
            );
        }

        [Fact]
        public void Program_ConfiguresPermissionsPolicyHeader()
        {
            var programSource = GetFileContent("restitution-app/Program.cs");

            Assert.Contains("Permissions-Policy", programSource);
            Assert.Contains(
                "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()",
                programSource
            );
        }

        [Fact]
        public void Caddyfile_ConfiguresPermissionsPolicyHeader()
        {
            var caddyfileContent = GetFileContent("restitution-app/ClientApp/Caddyfile");

            Assert.Contains("Permissions-Policy", caddyfileContent);
            Assert.Contains(
                "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()",
                caddyfileContent
            );
        }
    }
}
