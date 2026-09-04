# Plan — SEC-SECRETS-002 (ZAP_TARGET from repo vars)

## Summary

In `.github/workflows/zap-coast-restitution-dev-scan.yml`, replace the literal `ZAP_TARGET: https://coast-restitution-dev.silver.devops.bcgov` with `ZAP_TARGET: ${{ vars.ZAP_TARGET }}`. Add a short comment (or README/docs note adjacent to the workflow) stating that repository admins must set Actions variable `ZAP_TARGET` to the private development URL before `workflow_dispatch` scans can succeed. Confirm the YAML no longer contains that hostname (or any real silver.devops hostname). Append evidence. Do not invent a replacement hostname in the file.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Source | `vars.ZAP_TARGET` | Finding asks for vars.*; not a secret (URL is env location, still private) |
| Docs | Inline workflow comment + evidence | Operators need an explicit setup hint |
| Live var value | Out of PR | Keep real hostname out of git |

## Residual

- Scan fails until `ZAP_TARGET` is set in repo settings (expected)
- Self-hosted runner / ZAP stack unchanged

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
