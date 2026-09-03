# Tasks — CONFIG-001

Derive from `spec/spec.md` + `features/config-001-trivy-gate.feature`.

## Milestone 1 — blocking image scan

- [x] **TASK-001** — On `.github/workflows/cd-restitution-api.yml` Trivy step: set `exit-code: "1"` and `severity: CRITICAL,HIGH` (or current `trivy-action` equivalents). Covers `@R-01.1`.
- [x] **TASK-002** — Same inputs on `.github/workflows/cd-restitution-ui.yml`. Covers `@R-01.1`.
- [x] **TASK-003** — Keep SARIF upload; if the scan step can fail, upload with `if: always()` so `@R-01.2` still holds.
- [x] **TASK-004** — Append `docs/pr-evidence.md` for CONFIG-001 (criterion IDs `@R-01.1` `@R-01.2`). Do not overwrite prior slices.

## Backlog

- [ ] Re-introduce a PR-time filesystem/secret scan (related: F-TEST-002 / SEC-SECRETS-003) — **not this slice**
