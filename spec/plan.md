# Plan — F-TEST-004 (Database tests)

## Summary

Add `Database.Tests` (xUnit, net10) next to `restitution-app.Tests`. Cover `Database.Extensions.MemoryCache.GetOrSet` and `DynamicsTokenProviderOptions.GetDynamicsApiEndpointUrl`. Do not construct `ServiceClient` or call token HTTP.

## Architecture

```text
restitution-app.sln
  Database
  Database.Tests (new)
    MemoryCacheTests
    DynamicsTokenProviderOptionsTests
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Location | Repo-root `Database.Tests/` (sibling of `Database/`) | Matches F-TEST-003 layout |
| Network | None | Pilot: no live Dynamics |
| Token HTTP | Out of scope | AUTH-001 / later |

## Security & privacy

- Fixture URLs are fake (`https://example.test/...`)
- Residual: token acquire and ServiceClient still untested

## Test approach

- `dotnet test restitution-app/restitution-app.sln` (or the new project)
- `@R-04.1` `@R-04.2`
- Append `docs/pr-evidence.md`

## Rollout

- Merge to `development`; CI still build-only (F-TEST-001)

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
