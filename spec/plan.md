# Plan — AUTH-003 (anonymous health surface)

## Summary

Keep `/hc` anonymous for OpenShift probes, but Predicate (or separate `/hc/ready`) so anonymous responses only run/include checks tagged `self`/`process`. Dataverse (`dataverse`/`ready`) must not appear on the anonymous detailed payload. Prefer `HealthCheckOptions.Predicate` on `/hc` filtering to self/process; optionally map `/hc/ready` for Dataverse with auth or omit from this slice (AUTHZ-001 may cover remaining auth). Unit-test the predicate helper. Append evidence. Do not require auth on self liveness.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Anonymous `/hc` | Predicate: tags contain `self` or `process` | Probes keep working; no Dataverse detail |
| Dataverse | Not on anonymous `/hc` | Addresses disclosure; AUTHZ-001 may still want auth on ready |
| Auth on self | No | Must not break OpenShift probes |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-04 |
| Security (if required) | | |
