# Plan — CRYPTO-002 (Splunk TLS bypass)

## Summary

Prefer **removing** the Development-only `DangerousAcceptAnyServerCertificateValidator` assignment for Splunk HEC. If a local insecure path must remain, gate it solely on an explicit config/env flag (e.g. `SPLUNK_INSECURE_SSL=true` / configuration key), never on `env.IsDevelopment()` alone. Default path leaves `messageHandler` null / system validation. Add a unit/config test asserting Program does not enable the dangerous validator under Development without the opt-in. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Preferred fix | Remove Dev bypass | Matches assessment intent |
| Alternate | Explicit SPLUNK_INSECURE_SSL opt-in | Safer than IsDevelopment alone |
| Live Splunk | Out of scope | No Dynamics/Splunk required |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
