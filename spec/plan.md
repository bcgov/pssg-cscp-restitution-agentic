# Plan — DEP-002 (EOL netcore packages)

## Summary

Delete `Microsoft.NETCore.App` 2.2.8 and `Microsoft.NETCore.Jit` 2.0.8 from `restitution-app/restitution-app.csproj`. Run `dotnet build`. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Fix | Remove only | Framework comes from TargetFramework net10.0 |

## Test approach

- `dotnet build restitution-app/restitution-app.csproj`
- `@R-10.1` `@R-10.2`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
