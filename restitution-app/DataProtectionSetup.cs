#nullable enable
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gov.Cscp.VictimServices.Public;

/// <summary>
/// Configures ASP.NET Core Data Protection key ring persistence for container deployments.
///
/// When <c>KEY_RING_DIRECTORY</c> is set, the key ring is persisted to that directory and, because
/// Windows DPAPI is unavailable on Linux/OpenShift, protected at rest with an X.509 certificate loaded
/// from <c>KEY_RING_CERTIFICATE_PATH</c> (optionally password-protected via
/// <c>KEY_RING_CERTIFICATE_PASSWORD</c>). Outside Development, persistence without a usable certificate
/// fails closed at startup rather than silently writing plaintext key material to disk.
/// </summary>
public static class DataProtectionSetup
{
    public static void Configure(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment
    )
    {
        var keyRingDirectory = configuration["KEY_RING_DIRECTORY"];
        if (string.IsNullOrEmpty(keyRingDirectory))
        {
            return;
        }

        var dataProtectionBuilder = services
            .AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(keyRingDirectory));

        var certificate = LoadCertificate(configuration);
        if (certificate is not null)
        {
            dataProtectionBuilder.ProtectKeysWithCertificate(certificate);
        }
        else if (!environment.IsDevelopment())
        {
            throw new InvalidOperationException(
                "KEY_RING_DIRECTORY is configured but no usable certificate was found at "
                    + "KEY_RING_CERTIFICATE_PATH. Refusing to persist Data Protection keys without "
                    + "at-rest protection outside Development."
            );
        }
    }

    private static X509Certificate2? LoadCertificate(IConfiguration configuration)
    {
        var certificatePath = configuration["KEY_RING_CERTIFICATE_PATH"];
        if (string.IsNullOrEmpty(certificatePath) || !File.Exists(certificatePath))
        {
            return null;
        }

        var password = configuration["KEY_RING_CERTIFICATE_PASSWORD"];

        try
        {
            return string.IsNullOrEmpty(password)
                ? X509CertificateLoader.LoadCertificateFromFile(certificatePath)
                : X509CertificateLoader.LoadPkcs12FromFile(certificatePath, password);
        }
        catch (Exception ex) when (ex is CryptographicException or IOException or UnauthorizedAccessException)
        {
            return null;
        }
    }
}
