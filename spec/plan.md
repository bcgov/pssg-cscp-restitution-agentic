# Plan — CONFIG-001 (Trivy gate)

> Architecture and delivery approach for issue #1 / CONFIG-001.

## Summary

Assessment `CONFIG-001` described Trivy steps with `exit-code: "0"` on `ci-restitution.yml` and `build-template.yml`. Those files no longer contain Trivy. Current scans are `aquasecurity/trivy-action` on **CD** after image push:

- `.github/workflows/cd-restitution-api.yml`
- `.github/workflows/cd-restitution-ui.yml`

`trivy-action` defaults to a non-failing exit. We will set `exit-code: "1"` and `severity: CRITICAL,HIGH` (or equivalent action inputs) so CRITICAL/HIGH fail the job while still uploading SARIF.

## Architecture

```text
push to development (path-filtered)
  → CD job: build & push image to OpenShift registry
  → Trivy image scan (blocking CRITICAL/HIGH)
  → upload SARIF
```

CI (`ci-restitution.yml`) stays CodeQL + build; this slice does **not** re-add filesystem Trivy to CI (would overlap F-TEST-002 / future slices). Residual: PRs can merge without this image gate; the gate runs on CD after merge. Call that out in evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Where to fail | CD Trivy steps, not restored `build-template.yml` | Match current repo; assessment paths are stale |
| Threshold | CRITICAL,HIGH | Matches finding; avoids failing on LOW/UNKNOWN noise |
| SARIF | Keep upload-sarif after scan | @R-01.2; use `if: always()` on upload if needed so fail still publishes |
| Secrets | Do not change OCP4_* | Out of scope |

## Security & privacy

- Classification: public intake app; scanner output is vuln metadata, not form PII
- Residual: first CD run after this change may fail if current images have HIGH CVEs — intended
- Do not disable other gates to greenwash

## Test approach

- No application unit tests (YAML-only)
- Verification: workflow YAML contains `exit-code: "1"` (or documented equivalent) on both CD Trivy steps; SARIF upload remains
- Manual/CI: checkpoint gate + spec review on the impl PR
- Criterion IDs: `@R-01.1`, `@R-01.2`

## Rollout

- Merge to `development`; next API/UI CD run is the live proof
- If CD is red due to existing CVEs, that is CONFIG-001 working — remediate vulns in a later issue, do not revert `exit-code`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
