# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#23](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/23) — rapid assessment **AUTH-002**.

## Problem

Global cookie policy sets `MinimumSameSitePolicy = SameSiteMode.None`, which weakens default cross-site cookie protections unless a documented cross-site need exists.

## Outcome

`MinimumSameSitePolicy` is **`SameSiteMode.Lax`** (preferred) unless a documented reason requires `None`. An automated check fails if the policy is still `None`.

## Scope

### In scope

- Change cookie policy in `Program.cs` (or extracted helper) from `None` to `Lax`
- Unit/config test asserting not `None` / equals `Lax`
- Brief note in evidence if any residual cross-site cookie use remains

### Out of scope

- Broader auth redesign (AUTH-001 already shipped)
- AUTH-003 health endpoint anonymity

## Journeys

1. Policy is Lax — `features/auth-002-samesite-lax.feature` (@R-23.1)
2. Automated regression check — same feature (@R-23.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-04 |
| BA | Alex Rivera (simulated) | 2026-09-04 |
| QA (acceptance ownership) | | |
