# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#14](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/14) — rapid assessment **F-TEST-002**.

## Problem

The assessment found security scanners configured as non-blocking (`continue-on-error` on CodeQL/Sonar; Trivy `exit-code: 0`). Current `development` already removed Sonar and CodeQL `continue-on-error`, and CD Trivy image gates fail on CRITICAL/HIGH (CONFIG-001). **CI still has no blocking vulnerability filesystem scan**, so PR-time security scanning is weaker than the finding intended.

## Outcome

CI includes a **blocking Trivy filesystem vulnerability scan** (CRITICAL/HIGH fail the job). CodeQL remains without `continue-on-error`. Reviewers can see that PR CI fails on critical/high FS findings, not only CD image scans.

## Users & personas

| Persona | Goal |
| --- | --- |
| Security reviewer | Critical/high vulns fail CI, not just CD |
| Developer | Clear residual: CD image Trivy already from CONFIG-001 |

## Scope

### In scope

- Add Trivy filesystem scan step(s) to `ci-restitution.yml` with failing exit on CRITICAL/HIGH
- Confirm CodeQL analyze step has no `continue-on-error`
- Document that assessment Sonar/`continue-on-error` paths are already gone; CD image Trivy already blocking

### Out of scope

- SEC-SECRETS-003 dedicated secret-scanner exit (separate issue) unless the same Trivy step naturally covers secrets with a second call
- ZAP workflow redesign
- Disabling CodeQL to “make CI green”

## Journeys

1. CI FS scan blocks — `features/f-test-002-blocking-security-scans.feature` (@R-14.1)
2. CodeQL remains blocking — same feature (@R-14.2)

## Open questions

- [x] Prefer adding CI Trivy FS over inventing SonarCloud again.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
