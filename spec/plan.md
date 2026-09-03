# Plan — CONFIG-002 (CSP unsafe-*)

## Summary

Wrap the `Content-Security-Policy` Append middleware in `Program.cs` so it runs **only when `IsDevelopment()`**. Leave `UseCsp` for non-Development unchanged. Append evidence. Optional: small note in README. No Angular rewrite.

## Architecture

```text
Development → Append permissive CSP (existing string)
non-Development → NWebsec UseCsp only (no Append)
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Fix | Gate Append to Development | Removes weak dual header in prod; keeps local DX |
| Tests | Code review + evidence; optional unit if host testable | No WebApplicationFactory without Dataverse |

## Security & privacy

- Residual: Development still has unsafe-*; nonce/hash CSP later

## Test approach

- Diff review of Program.cs; `@R-07.1` `@R-07.2`
- Append `docs/pr-evidence.md`

## Rollout

- Merge to `development`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
