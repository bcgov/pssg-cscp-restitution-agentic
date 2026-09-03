# Plan — DEP-001 (archived HealthChecks package)

## Summary

Remove `PackageReference Include="Microsoft.AspNetCore.HealthChecks" Version="1.0.0"` from `restitution-app/restitution-app.csproj`. Custom checks already implement `Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheck`. Confirm `dotnet build` and that `/hc` mapping in `Program.cs` is unchanged.

## Architecture

```text
Program.cs AddHealthChecks + MapHealthChecks("/hc")
  → ApiSelfHealthCheck, DataverseHealthCheck
  → Microsoft.Extensions.Diagnostics.HealthChecks (in-box)
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Replacement | In-box Extensions library (no extra NuGet unless build requires it) | Already imported in HealthChecks/*.cs |
| Behaviour | Do not change JSON writer / path / tags | Out of AUTH-003 scope |
| Tests | Build + optional smoke that `/hc` still maps; no live Dataverse | Pilot |

## Security & privacy

- Removes unmaintained dependency from the API graph
- Residual: Dataverse check still needs credentials locally

## Test approach

- `dotnet build restitution-app/restitution-app.csproj`
- Criterion `@R-02.1` `@R-02.2`
- Append `docs/pr-evidence.md`

## Rollout

- Merge to `development`; no deploy-specific steps

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
