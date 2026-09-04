# Plan — AUTHZ-002 (UseAuthorization middleware)

## Summary

Register `AddAuthorization()` in DI and call `UseAuthorization()` after `UseRouting()` and before `MapControllers()` so `[Authorize]` attributes become enforceable. Do not add authentication schemes or protect existing controllers. Keep `/hc` anonymous (AUTHZ-001 status-only writer and AUTH-003 predicate unchanged). Add a unit/config test that Program source (or equivalent host composition assertion) contains `UseAuthorization`. Append `docs/pr-evidence.md`.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Middleware order | After UseRouting, before MapControllers | ASP.NET Core requirement for endpoint auth |
| Auth redesign | None | Finding is missing middleware only |
| `/hc` | Remain anonymous | OpenShift probes |
| Test approach | Source/config assertion on Program | No live auth needed |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
