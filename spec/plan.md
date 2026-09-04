# Plan — SEC-SECRETS-001 (README secrets placeholders)

## Summary

Edit `restitution-app/README.md` user-secrets JSON template only: replace the five hardcoded Dynamics / ADFS / Entra endpoint and resource URLs with angle-bracket placeholders such as `<dynamics-api-endpoint-url>`, `<adfs-token-endpoint>`, `<dynamics-resource-url>`, `<cloud-dynamics-api-endpoint-url>`, and `<cloud-dynamics-resource-url>` (or equivalent clear names). Keep existing credential placeholders. Do **not** invent or paste real hostnames. Append a short evidence receipt. Optionally add a lightweight regression check (grep/script or docs-facing assertion) that the template block no longer contains known hostname patterns — not required if evidence documents a manual scan of the template.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Change surface | README template only | Finding is documentation reconnaissance, not runtime |
| Replacement values | Angle-bracket placeholders | Matches existing credential placeholder style; no fake hostnames |
| Verification | Evidence + visual/grep of template | No app behaviour change; CI optional |

## Residual

- Developers need the real URLs from private runbooks / team channels (intentionally not in public README)
- SEC-SECRETS-002 (ZAP workflow) remains separate

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
