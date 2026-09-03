# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#13](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/13) — rapid assessment **F-TEST-001**.

## Problem

CI builds the Angular SPA and compiles the API but **never runs automated tests**. Unit suites added for the API and Database projects can fail locally without failing the pipeline.

## Outcome

The CI gate job **runs `dotnet test`** against the solution (API + Database test projects) after restore/build. A failing test fails the job. Path filters include the test project directories so changes to tests also trigger CI.

## Users & personas

| Persona | Goal |
| --- | --- |
| Delivery reviewer | Green CI means tests ran and passed |
| Developer | Test-only PRs still exercise CI |

## Scope

### In scope

- Add a `dotnet test` step to `.github/workflows/ci-restitution.yml` for `restitution-app/restitution-app.sln` (or equivalent covering both test projects)
- Extend workflow `paths` filters to include `restitution-app.Tests/**` and `Database.Tests/**`
- No live Dataverse required (existing tests are offline)

### Out of scope

- Angular Karma / Playwright in CI (F-TEST-005 / F-TEST-006)
- Making Trivy/CodeQL blocking beyond current CONFIG-001 work (F-TEST-002)
- Fixing unrelated CodeQL default-setup conflicts (record residual if gate still noisy)

## Journeys

1. Tests run in CI — `features/f-test-001-ci-dotnet-test.feature` (@R-13.1)
2. Path coverage — same feature (@R-13.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
