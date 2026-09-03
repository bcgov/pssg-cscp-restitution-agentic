# Tasks — DEP-001

## Milestone 1

- [x] **TASK-001** — Remove `Microsoft.AspNetCore.HealthChecks` 1.0.0 from `restitution-app/restitution-app.csproj`. Covers `@R-02.1`.
- [x] **TASK-002** — Confirm HealthChecks classes still compile against `Microsoft.Extensions.Diagnostics.HealthChecks`; add an explicit PackageReference only if the SDK does not supply it. Covers `@R-02.1`.
- [x] **TASK-003** — Do not change `/hc` path, JSON shaping, or check registration unless required to compile. Covers `@R-02.2`.
- [x] **TASK-004** — `dotnet build` the API project; append `docs/pr-evidence.md` (CONFIG-001 slice must remain).

## Backlog

- [ ] DEP-002 EOL netcore packages — **not this slice**
