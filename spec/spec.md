# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#26](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/26) — rapid assessment **AUTHZ-002**.

## Problem

The ASP.NET Core pipeline calls `UseRouting` and maps controllers but does not register authorization services or call `UseAuthorization`. Any future `[Authorize]` attribute on a controller or action would be silently ignored, creating an invisible authorization bypass when the app is extended.

## Outcome

Authorization is wired so `[Authorize]` is enforceable: services include authorization registration, and the middleware pipeline calls authorization after routing and before controller endpoints. Anonymous `/hc` (status-only liveness) remains reachable without authentication. This slice does not redesign authentication or add new protected APIs.

## Scope

### In scope

- Register authorization (`AddAuthorization` or equivalent)
- Call `UseAuthorization` after `UseRouting` and before `MapControllers` (or equivalent endpoint mapping order)
- Keep `/hc` anonymous
- Unit/config test asserting Program (or middleware composition) includes `UseAuthorization`
- Evidence note

### Out of scope

- Adding `[Authorize]` to existing controllers
- Redesigning ADFS / auth schemes
- Requiring auth on `/hc`
- Live Dynamics tests

## Journeys

1. Authorization middleware present — `features/authz-002-use-authorization.feature` (@R-26.1)
2. Health stays anonymous — same feature (@R-26.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
