# Plan — LOG-002 (success-path submit audit log)

## Summary

Add a small testable audit helper (e.g. `RestitutionSubmitAudit.WriteSuccess`) that calls `ILogger.LogInformation` with **non-PII** fields: form type (`victim` / `victim-entity` / `offender`), correlation id (`HttpContext.TraceIdentifier` or equivalent), and success=`true`. Call it from `RestitutionsController.SubmitRestitutionInternal` immediately before `return Ok(response)` on the Dynamics success path. Pass form type from each public submit action. Unit-test with a fake/mock `ILogger` asserting `LogInformation` / audit invocation and that message/state does not include the Dynamics `OrganizationResponse` or form PII payload. Append evidence. Do **not** change LOG-003 failure-path body logging in this slice.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| API | `ILogger` `LogInformation` (MEL) via helper | Testable with fake/mock logger; matches finding “application-level” audit |
| Fields | form type + correlation id + success | Non-PII operational trail; no case PII |
| Scope | Success path only | Finding is success-path gap; failure body logging is LOG-003 |
| Dynamics in tests | Not required | Mock logger + helper (or controller with stubbed org service if needed) |

## Residual

- Existing Serilog `Error` paths that destructure `OrganizationResponse` remain LOG-003
- No Splunk / sink changes

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
