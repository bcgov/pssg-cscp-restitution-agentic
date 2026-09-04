# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#34](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/34) — rapid assessment **SEC-SECRETS-003**.

## Problem

Assessment noted Trivy secret scanning with non-enforcing exit codes. Current `development` already has enforcing Trivy **vulnerability** FS scans in CI (`exit-code: 1`), but secret scanning is not an enforcing gate. Stale assessment paths (`build-template.yml`) no longer apply.

## Outcome

CI (and/or CD as proportionate) runs an enforcing Trivy **secret** scan (`scanners: secret`, `exit-code: 1`) so committed secrets fail the pipeline. Existing vuln FS enforcement remains. Do not disable Tier workflows.

## Scope

### In scope

- Add enforcing Trivy secret scan to CI restitution workflow (primary)
- Optionally mirror on CD if images/fs paths support secret scanners without false-positive lockout
- Evidence documenting assessment path drift

### Out of scope

- Disabling CodeQL or other Tier gates
- Broader secrets management / rotation
- Live Dynamics

## Journeys

1. Enforcing secret scan — `features/sec-secrets-003-trivy-secret.feature` (@R-34.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
