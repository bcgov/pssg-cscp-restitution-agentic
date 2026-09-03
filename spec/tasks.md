# Tasks — AUTH-001

## Milestone 1

- [ ] **TASK-001** — Replace password-token call in `ADFSTokenProvider` with client-credentials request (ClientId, ClientSecret, Resource/Scope as appropriate). Covers `@R-06.1` `@R-06.2`.
- [ ] **TASK-002** — Update `restitution-app/README.md` ADFS secrets example: remove ServiceAccountName/Password; note client-credentials requirement. Covers `@R-06.1`.
- [ ] **TASK-003** — Add `ADFSTokenProviderTests` with mocked HTTP: assert grant_type client_credentials, no username/password, token returned. Covers `@R-06.1` `@R-06.2`.
- [ ] **TASK-004** — `dotnet test Database.Tests`; append `docs/pr-evidence.md`. Covers `@R-06.2`.

## Backlog

- [ ] Live ADFS smoke — human / ops
- [ ] CRYPTO-003 / LOG-005 — **not this slice**
