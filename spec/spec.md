# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#6](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/6) — rapid assessment **AUTH-001**.

## Problem

On-premise Dynamics token acquisition still uses the **resource-owner password** grant: a service-account username and password are posted to ADFS. That grant is deprecated, blocks MFA / phishing-resistant policies, and is removed from OAuth 2.1. Cloud (Entra) already uses client credentials.

## Outcome

ADFS token acquisition uses a **client-credentials** style grant (client id + client secret only). Username and password are **not** sent on the token request. A unit test proves the password grant is gone without calling a live ADFS host. Operators must ensure the ADFS application allows client credentials (app registration is outside this repo).

## Users & personas

| Persona | Goal |
| --- | --- |
| Security reviewer | No ROPC on the Dataverse integration path |
| Platform / Dynamics admin | Clear residual: ADFS client must allow client credentials |

## Scope

### In scope (this release)

- Change `ADFSTokenProvider` to stop calling password-token APIs
- Use client id + client secret (+ resource/scope as already configured) instead
- Stop documenting / requiring service-account username+password for the ADFS token path in README secrets template
- Unit test with a mocked HTTP handler (no live ADFS)

### Out of scope

- Changing Entra / cloud path (already client credentials)
- Standing up or reconfiguring ADFS / Entra app registrations (constitution J2)
- Live Dynamics smoke
- CRYPTO-003 plaintext token cache
- LOG-005 token URL logging

## Journeys

1. Password grant removed — `features/auth-001-adfs-client-credentials.feature` (@R-06.1)
2. Client credentials used — same feature (@R-06.2)

## Non-functional requirements

- Privacy: tests use fake endpoints and secrets only
- Residual risk: on-prem environments must be configured for client credentials before deploy; otherwise token acquire fails closed

## Open questions

- [x] Prefer IdentityModel `RequestClientCredentialsTokenAsync` (same pattern as Entra) over inventing a custom form post.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
