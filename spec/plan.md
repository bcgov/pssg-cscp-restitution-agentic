# Plan — F-TEST-003 (first API tests)

## Summary

Add an xUnit test project that references `restitution-app` and covers `ConfigurationController.GetConfiguration` with `Microsoft.Extensions.Configuration` in-memory values. Do not boot the full web host (Dataverse is registered in `Program.cs`). Do not add a CI test stage (F-TEST-001).

## Architecture

```text
restitution-app.sln
  restitution-app
  Database
  restitution-app.Tests  (new)
    ConfigurationControllerTests
      → new ConfigurationController(logger, inMemoryConfig).GetConfiguration()
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Runner | xUnit + Microsoft.NET.Test.Sdk | Standard for net10 |
| Host | Controller unit test, not WebApplicationFactory | Avoid `AddDatabase` / live URI at startup |
| Surface | ConfigurationController only | No Dynamics; proves non-stub coverage |
| CI | Leave CI RESTITUTION as build-only | Owned by F-TEST-001 |

## Security & privacy

- Synthetic config strings only
- Residual: lookups and submit remain untested until later slices

## Test approach

- `dotnet test` on the new project
- Criterion `@R-03.1` `@R-03.2`
- Append `docs/pr-evidence.md` (keep CONFIG-001 and DEP-001 evidence)

## Rollout

- Merge to `development`; CI still does not run tests (documented residual)

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
