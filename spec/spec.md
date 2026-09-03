# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#16](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/16) — rapid assessment **F-TEST-006**.

## Problem

Playwright E2E tests exist under ClientApp but **CI never runs them**. Regressions in routing/health UI can merge unnoticed.

## Outcome

CI runs a **Playwright localhost suite** (at least `health-and-routing` scenarios) against a locally served SPA. A failing E2E fails the workflow. No dependency on live Dynamics or remote `dev.justice.gov.bc.ca`.

## Users & personas

| Persona | Goal |
| --- | --- |
| QA | Routing/health regressions fail PRs |
| Developer | E2E stays offline-capable |

## Scope

### In scope

- Add a CI job (or steps) in `ci-restitution.yml` that installs Playwright browsers and runs `--project=localhost` for `e2e/tests/health-and-routing.spec.ts` (minimum)
- Serve the SPA for that job (dev server or static serve of build output) without live Dynamics
- Mock/stub API/health as the existing specs already do where needed

### Out of scope

- Running `dev`/`test` remote projects against real environments
- Full victim/offender form submit E2E in CI
- Self-hosted ZAP

## Journeys

1. CI runs Playwright — `features/f-test-006-playwright-ci.feature` (@R-16.1)
2. Offline localhost only — same feature (@R-16.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
