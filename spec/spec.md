# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#24](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/24) — rapid assessment **AUTH-003**.

## Problem

`/hc` is anonymously reachable and returns detailed JSON including named checks (API, Dataverse) and connectivity descriptions, disclosing backend readiness to unauthenticated callers.

## Outcome

Anonymous access is limited to **self/process liveness** (OpenShift probes keep working). Dataverse/ready details are **not** on the anonymous detailed payload (filter by tag, or expose ready on a separate non-anonymous `/hc/ready`). Do **not** require full auth that breaks probes. AUTHZ-001 overlap may remain for later; fix AUTH-003 proportionately.

## Scope

### In scope

- Restrict anonymous `/hc` (or equivalent) to `self`/`process` tagged checks
- Keep OpenShift liveness working without auth
- Unit/config test for the predicate / mapping
- Note AUTHZ-001 may remain open

### Out of scope

- Full authorization redesign for all health endpoints (AUTHZ-001)
- Requiring auth on the self liveness probe
- Live Dynamics connectivity tests

## Journeys

1. Anonymous surface excludes Dataverse details — `features/auth-003-health-check-surface.feature` (@R-24.1)
2. Self probe stays anonymous — same feature (@R-24.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-04 |
| BA | Alex Rivera (simulated) | 2026-09-04 |
| QA (acceptance ownership) | | |
