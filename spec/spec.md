# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#8](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/8) — rapid assessment **CONFIG-003**.

## Problem

The developer exception page (full stack traces and request details) is enabled for **every non-Production** environment name — Staging, Testing, UAT, Development. Shared lower environments that are reachable externally can leak internals on unhandled errors.

## Outcome

The developer exception page runs **only in Development**. All other environments use the generic exception handler path (no detailed exception page). Reviewers can see the condition is `IsDevelopment()`, not `!IsProduction()`.

## Users & personas

| Persona | Goal |
| --- | --- |
| Security reviewer | No stack traces in shared Staging/Test |
| Local developer | Still get detailed exceptions when ASPNETCORE_ENVIRONMENT=Development |

## Scope

### In scope (this release)

- Change the exception-page gate in `Program.cs` from `!IsProduction()` to `IsDevelopment()`
- Keep `UseExceptionHandler` for non-Development
- Evidence note; mention LOG-001 / VULN-003 as related residual if still open

### Out of scope

- Changing `/Home/Error` UI
- LOG-001 / VULN-003 duplicate filings beyond this code fix
- Splunk logging changes

## Journeys

1. Development keeps detail — `features/config-003-dev-exception-page.feature` (@R-08.1)
2. Non-Development uses handler — same feature (@R-08.2)

## Non-functional requirements

- Privacy: stack traces must not appear on shared lower envs

## Open questions

- [x] Same code change likely closes LOG-001 / VULN-003 behaviourally; leave those issues to their own slices unless product asks to close as duplicate after this merges.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
