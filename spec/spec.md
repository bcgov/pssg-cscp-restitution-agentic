# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#4](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/4) — rapid assessment **F-TEST-004**.

## Problem

The Dataverse client library (`Database/`) has **no automated tests**. Token routing, cache wrap, and endpoint selection can only fail in a live Dynamics environment.

## Outcome

There is a **Database test project** that runs without a live Dataverse org. At least one test covers in-process cache behaviour, and at least one covers how the Dynamics API endpoint is chosen from authentication type. Tests must not call ADFS or Entra over the network.

## Users & personas

| Persona | Goal |
| --- | --- |
| Developer | Catch cache / endpoint-selection regressions without a Dynamics org |
| Security / QA | Proof the client library is no longer a zero-test surface |

## Scope

### In scope (this release)

- Add a test project targeting the `Database` assembly, included in `restitution-app/restitution-app.sln`
- Cover `MemoryCache.GetOrSet` (hit vs miss) with the in-memory cache
- Cover `DynamicsTokenProviderOptions.GetDynamicsApiEndpointUrl` for OnPremise vs Cloud
- `dotnet test` succeeds with no Dynamics URI or secrets

### Out of scope

- Live `ServiceClient` / WhoAmI / `AddDatabase` host spin-up
- Real ADFS or Entra token HTTP (AUTH-001)
- API controller tests (already F-TEST-003)
- CI test stage (F-TEST-001)
- Security-focused tests (F-TEST-007)

## Journeys

1. Tests exist — `features/f-test-004-database-tests.feature` (@R-04.1)
2. Cache and endpoint selection asserted — same feature (@R-04.2)

## Non-functional requirements

- Privacy: synthetic URLs and keys only; no real tenant IDs or secrets in fixtures
- Availability: tests run fully offline

## Open questions

- [x] Do not require HttpClient mocks for token providers in this slice. Endpoint options + cache are enough to close the “zero tests under Database/” finding.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
