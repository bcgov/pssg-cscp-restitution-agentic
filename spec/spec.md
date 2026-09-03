# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#11](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/11) — rapid assessment **DEP-003**.

## Problem

Neither the API nor Database project uses NuGet **lock files**. Transitive packages can resolve differently across machines and CI.

## Outcome

Both `restitution-app` and `Database` enable restore-with-lock-file and commit `packages.lock.json`. A restore/build succeeds with those locks present.

## Users & personas

| Persona | Goal |
| --- | --- |
| Security / supply-chain | Reproducible NuGet graph |
| Developer | Lock files checked in and documented |

## Scope

### In scope

- Set `RestorePackagesWithLockFile` on both csproj files
- Generate and commit `packages.lock.json` for both
- Document how to refresh locks in README or evidence

### Out of scope

- Forcing `RestoreLockedMode` in CI (nice-to-have if easy; not required)
- npm lock changes
- DEP-006 Dockerfile

## Journeys

1. Locks exist — `features/dep-003-nuget-lock-files.feature` (@R-11.1)
2. Build works — same feature (@R-11.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
