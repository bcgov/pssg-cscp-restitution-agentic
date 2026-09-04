# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#29](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/29) — rapid assessment **CRYPTO-002**.

## Problem

When the host runs as Development, Splunk HEC client configuration disables TLS certificate validation via `DangerousAcceptAnyServerCertificateValidator`. That gate is only `IsDevelopment()`, so a mis-set environment name in a non-isolated deployment would allow MITM on the log transport.

## Outcome

The Splunk TLS bypass is removed or gated behind an **explicit opt-in** configuration flag (e.g. `SPLUNK_INSECURE_SSL=true`), not bare `IsDevelopment()`. Prefer removing the Dev bypass entirely if local Splunk is not required; otherwise require the explicit opt-in. Default behaviour validates certificates.

## Scope

### In scope

- Remove or re-gate DangerousAcceptAnyServerCertificateValidator
- Explicit opt-in config if a bypass must remain for local debugging
- Unit/config test documenting the gating rule
- Evidence

### Out of scope

- Adding Splunk CA to trust stores in CI
- Live Splunk connectivity
- Changing other TLS paths

## Journeys

1. No ambient Development TLS bypass — `features/crypto-002-splunk-tls.feature` (@R-29.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
