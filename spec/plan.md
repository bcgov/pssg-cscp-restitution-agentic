# Plan — SEC-SECRETS-003 (enforcing Trivy secret scan)

## Summary

Add a dedicated Trivy filesystem step in `.github/workflows/ci-restitution.yml` with `scanners: secret` and `exit-code: "1"`. Prefer a separate step from the existing vuln FS scan so severity filters for vulns are unchanged. Run with `if: always()` after CodeQL when practical so a CodeQL residual does not skip the secret gate. Document that assessment `build-template.yml` / exit-code 0 comments are stale. Skip CD image secret scan unless trivially safe — CI FS is the proportionate enforce point.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Primary gate | CI FS secret scanner | Always runs on PRs |
| Separate step | Yes | Keep vuln CRITICAL,HIGH filter intact |
| CD | Optional / skip if noisy | Prefer CI enforce |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
