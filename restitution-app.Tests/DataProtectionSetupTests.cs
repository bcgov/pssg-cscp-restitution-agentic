using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Gov.Cscp.VictimServices.Public;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class DataProtectionSetupTests
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

        private static IConfiguration BuildConfiguration(Dictionary<string, string?> settings) =>
            new ConfigurationBuilder().AddInMemoryCollection(settings).Build();

        private static string CreateTemporaryPfx(string password)
        {
            using var rsa = RSA.Create(2048);
            var request = new CertificateRequest(
                "CN=restitution-app-tests",
                rsa,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1
            );

            using var certificate = request.CreateSelfSigned(
                DateTimeOffset.UtcNow.AddDays(-1),
                DateTimeOffset.UtcNow.AddDays(1)
            );

            var pfxPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pfx");
            File.WriteAllBytes(pfxPath, certificate.Export(X509ContentType.Pfx, password));

            return pfxPath;
        }

        [Fact]
        public void Configure_WithoutKeyRingDirectory_DoesNotRegisterDataProtection()
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(new Dictionary<string, string?>());

            DataProtectionSetup.Configure(services, configuration, new FakeHostEnvironment("Development"));

            Assert.DoesNotContain(
                services,
                d => d.ServiceType.FullName == "Microsoft.AspNetCore.DataProtection.IKeyManager"
            );
        }

        [Fact]
        public void Configure_WithDirectoryAndCertificate_ProtectsKeysWithCertificate()
        {
            var keyRingDirectory = Directory.CreateTempSubdirectory().FullName;
            var pfxPath = CreateTemporaryPfx("testpass");

            try
            {
                var services = new ServiceCollection();
                var configuration = BuildConfiguration(
                    new Dictionary<string, string?>
                    {
                        ["KEY_RING_DIRECTORY"] = keyRingDirectory,
                        ["KEY_RING_CERTIFICATE_PATH"] = pfxPath,
                        ["KEY_RING_CERTIFICATE_PASSWORD"] = "testpass",
                    }
                );

                DataProtectionSetup.Configure(services, configuration, new FakeHostEnvironment("Production"));

                using var provider = services.BuildServiceProvider();
                var keyManager =
                    provider.GetRequiredService<Microsoft.AspNetCore.DataProtection.KeyManagement.IKeyManager>();

                Assert.NotNull(keyManager);
            }
            finally
            {
                File.Delete(pfxPath);
                Directory.Delete(keyRingDirectory, recursive: true);
            }
        }

        [Fact]
        public void Configure_WithDirectoryButNoCertificate_ThrowsOutsideDevelopment()
        {
            var keyRingDirectory = Directory.CreateTempSubdirectory().FullName;

            try
            {
                var services = new ServiceCollection();
                var configuration = BuildConfiguration(
                    new Dictionary<string, string?> { ["KEY_RING_DIRECTORY"] = keyRingDirectory }
                );

                Assert.Throws<InvalidOperationException>(() =>
                    DataProtectionSetup.Configure(services, configuration, new FakeHostEnvironment("Production"))
                );
            }
            finally
            {
                Directory.Delete(keyRingDirectory, recursive: true);
            }
        }

        [Fact]
        public void Configure_WithDirectoryButNoCertificate_DoesNotThrowInDevelopment()
        {
            var keyRingDirectory = Directory.CreateTempSubdirectory().FullName;

            try
            {
                var services = new ServiceCollection();
                var configuration = BuildConfiguration(
                    new Dictionary<string, string?> { ["KEY_RING_DIRECTORY"] = keyRingDirectory }
                );

                var exception = Record.Exception(() =>
                    DataProtectionSetup.Configure(services, configuration, new FakeHostEnvironment("Development"))
                );

                Assert.Null(exception);
            }
            finally
            {
                Directory.Delete(keyRingDirectory, recursive: true);
            }
        }
    }
}
