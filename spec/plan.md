# Plan — LOG-003 (failure log without full response body)

## Summary

Replace the RestitutionsController failure path that logs `{@Response}` with a structured error log that records `IsSuccess` and error-code / result-key metadata only (no Serilog destructuring of the full OrganizationResponse). Prefer extending `RestitutionSubmitAudit` (or a tiny sibling helper) so the controller stays thin and unit tests can assert message templates and arguments without standing up Dynamics. Keep HTTP 500 behaviour unchanged.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Approach | Structured scalars / keys, not `{@Response}` | Matches finding |
| Helper | Extend RestitutionSubmitAudit | Same audit surface as LOG-002 |
| Success path | Unchanged | Out of scope |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
