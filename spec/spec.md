# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#2](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/2) — rapid assessment **DEP-001**.

## Problem

The API still pulls in an **archived, unmaintained health-check package** from 2017. Process liveness and Dataverse connectivity checks already use the platform’s built-in health-check types. The old package is leftover dependency risk: no maintainer, no security updates.

## Outcome

The shipped application **does not reference** the archived community health-check package. Health endpoints keep working using the supported platform library. A reviewer can see that package is absent from the project file.

## Users & personas

| Persona | Goal |
| --- | --- |
| Operator / platform | `/hc` still reports API process status (and Dataverse status when configured) |
| Security reviewer | No archived HealthChecks 1.0.0 on the production graph |

## Scope

### In scope (this release)

- Remove the archived package reference
- Keep existing health-check behaviour (API self-check + Dataverse check, JSON `/hc`)
- Use only the in-box / currently supported health-check APIs already used in source

### Out of scope

- Changing `/hc` auth (AUTH-003 / AUTHZ-001)
- Replacing Dataverse WhoAmI with a mock (unless required to compile)
- DEP-002 EOL `Microsoft.NETCore.App` / JIT packages
- Adding new health checks

## Journeys

1. Package gone — `features/dep-001-healthchecks-inbox.feature` (@R-02.1)
2. Health endpoint still serves — same feature (@R-02.2)

## Non-functional requirements

- Accessibility: n/a
- Privacy: health payload must not grow to include extra PII
- Availability: `/hc` remains reachable for probes

## Open questions

- [x] Source already implements `IHealthCheck` from `Microsoft.Extensions.Diagnostics.HealthChecks`. This slice is primarily **removing the unused archived PackageReference**.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
