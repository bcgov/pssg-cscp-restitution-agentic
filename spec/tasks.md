# Tasks — F-TEST-003

## Milestone 1

- [x] **TASK-001** — Add `restitution-app.Tests` (xUnit, net10.0) under `restitution-app/` (or sibling) and include it in `restitution-app/restitution-app.sln`. Covers `@R-03.1`.
- [x] **TASK-002** — ProjectReference the API project; InternalsVisibleTo only if needed. Do not change public JSON of configuration. Covers `@R-03.1`.
- [x] **TASK-003** — Add `ConfigurationControllerTests` that set in-memory `CONFIGURATION_*` keys, call `GetConfiguration`, and assert HTTP 200 plus `MaintenanceMode` / outage fields. Covers `@R-03.2`.
- [x] **TASK-004** — `dotnet test` the new project without Dataverse env vars; append `docs/pr-evidence.md`. Covers `@R-03.1`.

## Backlog

- [ ] F-TEST-001 CI test stage — **not this slice**
- [ ] F-TEST-004 Database tests — **not this slice**
