# Plan — AUTH-001 (ADFS client credentials)

## Summary

Replace `RequestPasswordTokenAsync` / `PasswordTokenRequest` in `Database/Extensions/ADFSTokenProvider.cs` with `RequestClientCredentialsTokenAsync` / `ClientCredentialsTokenRequest` (mirror Entra). Remove ServiceAccountName/Password from README secrets template for ADFS. Add `ADFSTokenProviderTests` with `HttpMessageHandler` mock asserting `grant_type=client_credentials` and absence of username/password. Optional: mark ServiceAccount* options obsolete or leave unused for config backward-compat without reading them.

## Architecture

```text
ADFSTokenProvider.AcquireTokenInternal
  → HttpClient.RequestClientCredentialsTokenAsync
  → ClientId + ClientSecret + Resource/Scope
  → no UserName / Password
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Grant | Client credentials | Matches Entra; removes ROPC |
| Options fields | Leave properties but unused (or Obsolete) | Avoid breaking deserialization of old secrets JSON |
| Live ADFS | Not required | Pilot; residual ops config |
| Tests | Mock handler in Database.Tests | No network |

## Security & privacy

- Removes password grant from code path
- Residual: ADFS app must allow client credentials; ops outside repo

## Test approach

- `dotnet test Database.Tests`
- `@R-06.1` `@R-06.2`
- Append `docs/pr-evidence.md`

## Rollout

- Merge to `development`; human smoke against on-prem ADFS when credentials available

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
