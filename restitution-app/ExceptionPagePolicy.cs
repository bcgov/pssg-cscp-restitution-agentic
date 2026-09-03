#nullable enable
using Microsoft.Extensions.Hosting;

namespace Gov.Cscp.VictimServices.Public;

/// <summary>
/// Testable policy for whether the ASP.NET Core developer exception page (full stack traces, request
/// headers/body, etc.) may be registered for a given environment.
///
/// The developer exception page is only ever allowed in Development. Staging/Test and Production must
/// use the generic error handler (<c>/Home/Error</c>) so unhandled exceptions never disclose sensitive
/// diagnostic detail to the client. See LOG-001 / CONFIG-003.
/// </summary>
public static class ExceptionPagePolicy
{
    public static bool AllowDeveloperExceptionPage(IHostEnvironment environment) => environment.IsDevelopment();
}
