# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#7](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/7) — rapid assessment **CONFIG-002**.

## Problem

Every response gets a Content-Security-Policy that allows **`unsafe-eval` and `unsafe-inline` in script-src**. In non-development, a stricter CSP also exists, but the weak header is still sent, so browsers see a permissive policy and XSS hardening depends on the second header staying correct.

## Outcome

Non-development responses **do not** advertise `unsafe-eval` or `unsafe-inline` in script-src via that always-on CSP middleware. Production continues to use the existing stricter CSP configuration. Reviewers can see the weak directives are gone from the production path.

## Users & personas

| Persona | Goal |
| --- | --- |
| Security reviewer | No dual weak CSP in production |
| Developer | Local Angular may still need a looser CSP in Development |

## Scope

### In scope (this release)

- Stop emitting the permissive always-on CSP (with unsafe-eval / unsafe-inline) on non-Development responses — preferred: Development-only for that middleware, or delete unsafe-* from it and rely on NWebsec in non-Development
- Leave NWebsec `UseCsp` for non-Development intact unless a conflict requires a tiny fix
- Evidence / note residual if Angular needs unsafe-* only in Development

### Out of scope

- Full nonce/hash CSP redesign for Angular
- Permissions-Policy (CONFIG-004)
- Developer exception page (CONFIG-003)

## Journeys

1. Production path — `features/config-002-csp-unsafe.feature` (@R-07.1)
2. Development path — same feature (@R-07.2)

## Non-functional requirements

- Do not weaken the existing non-Development NWebsec CSP
- Residual: Development may still use unsafe-* for local tooling

## Open questions

- [x] Prefer gating the Append middleware to Development over deleting NWebsec.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
