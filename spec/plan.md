# Plan — DEP-003 (NuGet lock files)

## Summary

Add `<RestorePackagesWithLockFile>true</RestorePackagesWithLockFile>` to `restitution-app.csproj` and `Database.csproj`. Run `dotnet restore --force-evaluate` (or equivalent) to generate `packages.lock.json` for each. Commit locks. Append evidence. Optional: note refresh command in README.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Scope | API + Database | Finding cites both |
| CI locked mode | Optional | Avoid breaking CI in this slice |

## Test approach

- `dotnet restore` + `dotnet build`
- `@R-11.1` `@R-11.2`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
