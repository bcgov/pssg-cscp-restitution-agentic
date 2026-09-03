# Plan — F-TEST-002 (blocking CI security scans)

## Summary

Add `aquasecurity/trivy-action` filesystem scan to `ci-restitution.yml` after checkout/build (scan repo or published paths) with `severity: CRITICAL,HIGH` and `exit-code: "1"`. Keep SARIF upload with `if: always()` if used. Verify CodeQL has no `continue-on-error`. Append evidence noting CONFIG-001 CD image gates and removed Sonar.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Scanner | Trivy FS in CI | Matches finding; CD already has image Trivy |
| Sonar | Do not re-add | Absent from current repo |

## Residual

- First CI run may fail on existing CRITICAL/HIGH deps — treat as intended gate; do not disable workflows
- CodeQL default-setup conflict remains a separate residual

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
