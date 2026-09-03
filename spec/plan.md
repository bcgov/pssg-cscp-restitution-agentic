# Plan — F-TEST-001 (CI dotnet test)

## Summary

In `ci-restitution.yml`, after `dotnet build`, add `dotnet test restitution-app/restitution-app.sln -c Release --no-build` (or build+test). Extend `on.pull_request.paths` / `on.push.paths` with `restitution-app.Tests/**`, `Database.Tests/**`, and optionally the workflow file. Append evidence. Do not add Karma/Playwright.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Command | Solution `dotnet test` | Covers API + Database tests from F-TEST-003/004 |
| Angular | Out of scope | F-TEST-005/006 |

## Test approach

- Diff review; optional local `dotnet test`
- `@R-13.1` `@R-13.2`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
