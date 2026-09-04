#nullable enable
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Gov.Cscp.VictimServices.Public;

/// <summary>
/// Testable application-level audit log for restitution form submissions.
///
/// Only non-PII operational identifiers are written so the audit trail never contains form content
/// or the Dynamics <c>OrganizationResponse</c> body. See LOG-002 / LOG-003.
/// </summary>
public static class RestitutionSubmitAudit
{
    public const string VictimFormType = "victim";
    public const string VictimEntityFormType = "victim-entity";
    public const string OffenderFormType = "offender";

    public static void WriteSuccess(ILogger logger, string formType, string? correlationId)
    {
        logger.LogInformation(
            "Restitution submission succeeded. FormType={FormType}, CorrelationId={CorrelationId}, Success={Success}",
            formType,
            string.IsNullOrEmpty(correlationId) ? "unknown" : correlationId,
            true
        );
    }

    /// <summary>
    /// Logs Dynamics submit failure using scalar IsSuccess / error-code fields and result key names only.
    /// Does not accept or destructure an OrganizationResponse body (LOG-003).
    /// </summary>
    public static void WriteDynamicsFailure(
        ILogger logger,
        object? isSuccess,
        object? errorCode,
        IEnumerable<string>? resultKeys
    )
    {
        var keys = resultKeys?.Where(k => !string.IsNullOrEmpty(k)).ToArray() ?? [];
        logger.LogError(
            "Error while saving victim restitution. Dynamics IsSuccess={IsSuccess}, ErrorCode={ErrorCode}, ResultKeys={ResultKeys}",
            isSuccess ?? "(missing)",
            errorCode ?? "(none)",
            keys.Length == 0 ? "(none)" : string.Join(",", keys)
        );
    }
}
