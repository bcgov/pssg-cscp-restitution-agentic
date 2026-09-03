# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#5](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/5) — rapid assessment **F-TEST-007**.

## Problem

Existing UI tests cover happy-path forms. **No test asserts a security control** (rejected file types, injection, unauthenticated API, CSRF, rate limits). Upload type checks live only in the Angular uploader.

## Outcome

There is at least one **security-focused automated test** that fails if a disallowed file type is accepted into the attachment list. It runs without Dataverse and without a live browser against production.

## Users & personas

| Persona | Goal |
| --- | --- |
| Security reviewer | Evidence that an upload control is actually tested |
| Developer | Regression net on extension allow-list |

## Scope

### In scope (this release)

- Replace or extend the file-uploader unit spec so it is not a create-only stub
- Assert that a file whose extension is not on the accepted list is **not** added to documents
- Assert that an oversized file is **not** added (existing 2MB cap) — still a security-relevant control
- Tests use synthetic `File` objects; no real PII

### Out of scope

- Server-side MIME checks (**VULN-002**)
- Playwright e2e security scenarios / CI Playwright (**F-TEST-006**)
- CSRF, rate-limit, unauthenticated API rejection (public intake has no user IdP)
- Wiring Angular tests into CI RESTITUTION (**F-TEST-001**)
- Filling remaining Karma stubs (**F-TEST-005**) beyond this component

## Journeys

1. Security test exists — `features/f-test-007-security-tests.feature` (@R-05.1)
2. Disallowed upload is rejected — same feature (@R-05.2)

## Non-functional requirements

- Privacy: synthetic filenames only
- Tests must not require Dynamics

## Open questions

- [x] Prefer existing Karma/Jasmine `file-uploader.component.spec.ts` over new Playwright so this slice stays local and CI-optional.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
