# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#25](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/25) — rapid assessment **AUTHZ-001**.

## Problem

Anonymous `/hc` still returns a JSON document with named check entries and description strings. Even after AUTH-003 limited which checks run, that payload can disclose process/API readiness detail to unauthenticated callers. Full authorization on the OpenShift liveness probe would break probes.

## Outcome

Anonymous `/hc` returns a **status-only** body (overall healthy / unhealthy style signal) with **no** check-name list and **no** description strings. OpenShift-style anonymous probes keep working (HTTP status without auth). Detailed ready / Dataverse health is **not** on the anonymous surface (already filtered by AUTH-003; do not re-expose names/descriptions). AUTHZ-002 (`UseAuthorization`) may land separately.

## Scope

### In scope

- Slim anonymous `/hc` ResponseWriter (or equivalent) to status-only JSON
- Keep `/hc` anonymous for self/process liveness
- Unit/config test asserting anonymous payload has no check names/descriptions
- Evidence note that AUTH-003 predicate remains

### Out of scope

- Requiring authentication on the self liveness probe
- Full authorization middleware pipeline (AUTHZ-002 #26)
- Live Dynamics connectivity tests
- New authenticated `/hc/ready` surface (optional later; not required if anonymous disclosure is removed)

## Journeys

1. Anonymous `/hc` status-only — `features/authz-001-health-status-only.feature` (@R-25.1)
2. Probes stay anonymous — same feature (@R-25.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
