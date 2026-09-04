# Tasks — SEC-SECRETS-002

## Milestone 1

- [x] **TASK-001** — Replace hardcoded `ZAP_TARGET` in `zap-coast-restitution-dev-scan.yml` with `${{ vars.ZAP_TARGET }}`; ensure no real hostname remains in that YAML. Covers `@R-20.1`.
- [x] **TASK-002** — Document that repository admins must set Actions variable `ZAP_TARGET` (workflow comment and/or short ops note). Covers `@R-20.2`.
- [x] **TASK-003** — Append `docs/pr-evidence.md`. Covers both.

## Backlog

- [ ] Operators set `ZAP_TARGET` in GitHub repo settings (human, private)
