# Plan — AUTHZ-001 (anonymous /hc status-only)

## Summary

Keep `/hc` anonymous for OpenShift probes. Change the anonymous ResponseWriter so the JSON body carries only an overall **status** signal (no `checks` array with names/descriptions). Retain AUTH-003 `Predicate` filtering to self/process so Dataverse checks still do not run on this surface. Unit/config test asserts the writer shape (and that RequireAuthorization is not applied to `/hc`). Append evidence. Do not add UseAuthorization in this slice (AUTHZ-002).

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Anonymous body | Status-only JSON | Removes residual disclosure after AUTH-003 |
| Auth on `/hc` | Still none | Must not break OpenShift probes |
| `/hc/ready` | Not required this slice | Disclosure fixed without new authenticated surface |
| AUTHZ-002 | Separate | Middleware is a different finding |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
