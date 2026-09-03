# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#9](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/9) — rapid assessment **CRYPTO-001**.

## Problem

When a key-ring directory is configured, Data Protection keys are written to disk **without an at-rest protector**. On Linux/OpenShift that means plaintext key XML on the volume.

## Outcome

Whenever keys are persisted to the filesystem, they are **also protected at rest** using an X.509 certificate (or equivalent ProtectKeysWith* provider suitable for OpenShift). Configuration documents the new certificate setting. Local Development without a key-ring directory remains unchanged (ephemeral keys).

## Users & personas

| Persona | Goal |
| --- | --- |
| Security reviewer | No plaintext key material on the volume |
| Platform operator | Clear config knobs for cert mount |

## Scope

### In scope (this release)

- Chain `ProtectKeysWithCertificate` (or documented ProtectKeysWith*) when persisting keys to the filesystem
- Add configuration key(s) for certificate path (and optional password) — no secrets committed
- Update README secrets / env docs
- Fail closed if persistence is enabled without a usable certificate in non-Development (preferred)

### Out of scope

- Standing up Azure Key Vault (unless already wired — it is not)
- CRYPTO-002 Splunk TLS
- Rotating existing production key rings (ops runbook residual)

## Journeys

1. Protected persist — `features/crypto-001-key-ring-protect.feature` (@R-09.1)
2. Local without directory — same feature (@R-09.2)

## Non-functional requirements

- Privacy: certificate password only via secrets/env
- Residual: ops must mount a cert before enabling KEY_RING_DIRECTORY in shared envs

## Open questions

- [x] Prefer filesystem X.509 over inventing Key Vault for this brownfield OpenShift app.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
