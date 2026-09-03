# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#3](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/3) — rapid assessment **F-TEST-003**.

## Problem

The ASP.NET Core API has **no automated tests**. Controllers that expose configuration, lookups, and restitution submission have never been verified by a test runner. Regressions in public JSON and input handling can only be caught by humans.

## Outcome

There is a **first API test project** that runs without live Dataverse. At least one automated test asserts a real controller behaviour (configuration / maintenance flags), not an empty stub. Reviewers can run the tests locally and see them pass.

## Users & personas

| Persona | Goal |
| --- | --- |
| Developer | Catch API configuration regressions before merge |
| Security / QA | Proof that the API is no longer a zero-test surface |

## Scope

### In scope (this release)

- Add an API test project (xUnit or equivalent) wired into the existing solution
- Cover `GET /api/configuration` (or the `ConfigurationController` equivalent) with in-memory configuration — no Dynamics
- At least one `*Tests.cs` (or equivalent) that fails if maintenance-mode / feature-flag mapping is wrong

### Out of scope

- Wiring `dotnet test` into CI RESTITUTION (**F-TEST-001**)
- Dataverse client / token-provider tests (**F-TEST-004**)
- Security-focused tests across Angular/API (**F-TEST-007**)
- Playwright / e2e
- Live Dataverse or submission write-path tests
- Changing production controller behaviour except if required to make it testable without altering public JSON

## Journeys

1. Tests exist — `features/f-test-003-api-tests.feature` (@R-03.1)
2. Configuration mapping is asserted — same feature (@R-03.2)

## Non-functional requirements

- Accessibility: n/a
- Privacy: tests use synthetic config values only; no live PII or Dynamics secrets
- Availability: tests must run offline (`dotnet test`)

## Open questions

- [x] Full `WebApplicationFactory` host likely registers Dataverse at startup. Prefer **controller unit tests** with in-memory `IConfiguration` unless the agent can stub `AddDatabase` cleanly without live URI.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
