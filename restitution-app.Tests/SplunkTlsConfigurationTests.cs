using System;
using System.IO;
using Xunit;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class SplunkTlsConfigurationTests
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
        public void ProgramSource_DoesNotDisableSplunkCertificateValidationForDevelopment()
        {
            var programPath = GetFileContentPath("restitution-app/Program.cs");
            var source = File.ReadAllText(programPath);

            // CRYPTO-002: TLS certificate validation for the Splunk HEC sink must never be
            // disabled solely because the environment is Development. Any remaining bypass
            // requires an explicit opt-in configuration flag, not bare IsDevelopment().
            Assert.DoesNotContain("DangerousAcceptAnyServerCertificateValidator", source, StringComparison.Ordinal);
        }
    }
}
