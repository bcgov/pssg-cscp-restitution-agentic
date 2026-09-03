# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#10](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/10) — rapid assessment **DEP-002**.

## Problem

The net10.0 API project still lists **EOL** `Microsoft.NETCore.App` 2.2.8 and `Microsoft.NETCore.Jit` 2.0.8 package references — leftover migration noise that confuses dependency review.

## Outcome

Those two PackageReferences are **removed**. The project still targets net10.0 and builds. Reviewers see no EOL meta-packages in the csproj.

## Users & personas

| Persona | Goal |
| --- | --- |
| Security / dependency reviewer | Clean net10 graph without EOL meta-packages |
| Developer | Build still succeeds |

## Scope

### In scope

- Remove both PackageReferences from `restitution-app.csproj`
- Confirm `dotnet build` succeeds

### Out of scope

- DEP-003 lock files
- DEP-006 OpenShift base image vs net10
- Broader dependency upgrades

## Journeys

1. Packages gone — `features/dep-002-eol-netcore-packages.feature` (@R-10.1)
2. Build still works — same feature (@R-10.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
