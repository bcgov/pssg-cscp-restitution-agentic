# Plan — CRYPTO-001 (key ring at-rest protection)

## Summary

When `KEY_RING_DIRECTORY` is set, load an X.509 cert from `KEY_RING_CERTIFICATE_PATH` (optional `KEY_RING_CERTIFICATE_PASSWORD`) and chain `.ProtectKeysWithCertificate(cert)` after `PersistKeysToFileSystem`. Document in README. If directory is set but cert cannot be loaded, throw at startup in non-Development. Development without directory unchanged.

## Architecture

```text
KEY_RING_DIRECTORY set
  → PersistKeysToFileSystem
  → ProtectKeysWithCertificate(X509 from path)
KEY_RING_DIRECTORY unset
  → skip (ephemeral / default DP)
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Protector | X.509 file | OpenShift-friendly; no new Azure dependency |
| Fail closed | Yes when persist enabled without cert | Avoid silent plaintext |

## Test approach

- Diff + evidence; optional unit that registration throws without cert when directory set
- `@R-09.1` `@R-09.2`

## Rollout

- Ops must mount cert before enabling key ring volume in shared envs

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
