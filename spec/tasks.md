# Tasks — F-TEST-002

## Milestone 1

- [ ] **TASK-001** — Add blocking Trivy filesystem scan to `ci-restitution.yml` (`severity: CRITICAL,HIGH`, `exit-code: "1"`). Covers `@R-14.1`.
- [ ] **TASK-002** — Confirm CodeQL analyze has no `continue-on-error`. Covers `@R-14.2`.
- [ ] **TASK-003** — Append `docs/pr-evidence.md` (note CONFIG-001 CD Trivy; Sonar absent). Covers both.

## Backlog

- [ ] SEC-SECRETS-003 secret-specific exit — **not this slice** unless trivial second Trivy secret step
