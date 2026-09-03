# Tasks — CONFIG-002

Derive from `spec/spec.md` + `features/config-002-csp-unsafe.feature`.

## Milestone 1 — gate weak CSP to Development

- [ ] **TASK-001** — In `restitution-api/Program.cs` (or equivalent host), wrap the always-on `Content-Security-Policy` Append middleware so it runs **only when `IsDevelopment()`**. Covers `@R-07.1` `@R-07.2`.
- [ ] **TASK-002** — Leave NWebsec `UseCsp` for non-Development unchanged (no weaken / delete unless a conflict requires a tiny fix). Covers `@R-07.1`.
- [ ] **TASK-003** — Optional: brief README note that Development may still use unsafe-* for local Angular tooling. Covers `@R-07.2`.
- [ ] **TASK-004** — Append `docs/pr-evidence.md` for CONFIG-002 (`@R-07.1` `@R-07.2`); do not overwrite prior slices.

## Backlog

- [ ] Nonce/hash CSP redesign for Angular — **not this slice**
- [ ] CONFIG-003 / CONFIG-004 — **not this slice**
