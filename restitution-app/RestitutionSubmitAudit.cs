#nullable enable
using Microsoft.Extensions.Logging;

namespace Gov.Cscp.VictimServices.Public;

/// <summary>
/// Testable application-level audit log for restitution form submissions.
///
/// Only non-PII operational identifiers are written (form type, correlation id, success flag) so the
/// audit trail never contains form content or the Dynamics <c>OrganizationResponse</c> body. See LOG-002.
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
}
