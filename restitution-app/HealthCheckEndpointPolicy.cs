#nullable enable
using System;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Gov.Cscp.VictimServices.Public;

/// <summary>
/// Predicates for which health checks may appear on the anonymous OpenShift liveness surface (AUTH-003).
/// </summary>
public static class HealthCheckEndpointPolicy
{
    public static readonly string[] AnonymousLivenessTags = { "self", "process" };

    public static bool IsAnonymousLivenessCheck(HealthCheckRegistration registration)
    {
        if (registration is null)
        {
            return false;
        }

        return registration.Tags.Any(tag => AnonymousLivenessTags.Contains(tag, StringComparer.OrdinalIgnoreCase));
    }
}
