# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#15](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/15) — rapid assessment **F-TEST-005**.

## Problem

Several Angular unit specs are **boilerplate stubs** (`expect(true).toEqual(true)` / create-only). They do not assert component behaviour.

## Outcome

At least the **NotFound** component spec asserts real behaviour (navigation to `/404` on construct). Remaining stubs may stay for later; this slice proves meaningful Angular unit coverage beyond create stubs. File-uploader security tests from F-TEST-007 remain.

## Users & personas

| Persona | Goal |
| --- | --- |
| QA / developer | Specs that fail when behaviour regresses |
| Security reviewer | Fewer always-green stubs in the SPA suite |

## Scope

### In scope

- Replace `not-found.component.spec.ts` stub with assertions on Router navigation to `/404`
- Optionally fix one additional stub if low-cost (field or app component)
- Tests run via existing Karma/`ng test` tooling locally (CI Angular tests still F-TEST-001 residual / later)

### Out of scope

- Converting every stub in the repo
- Wiring `ng test` into CI (follow-up)
- Playwright (F-TEST-006)

## Journeys

1. NotFound is meaningful — `features/f-test-005-angular-unit-substance.feature` (@R-15.1)
2. Stub gone — same feature (@R-15.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
