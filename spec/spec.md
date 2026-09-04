# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#20](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/20) — rapid assessment **SEC-SECRETS-002**.

## Problem

The OWASP ZAP development scan workflow hardcodes the development environment hostname as `ZAP_TARGET`. That hostname reveals a live internal OpenShift Silver Cluster deployment path in a public repository.

## Outcome

`ZAP_TARGET` is sourced from a repository variable (for example `${{ vars.ZAP_TARGET }}`), not a literal hostname in workflow YAML. Documentation states that operators must set the variable in repo settings before the scan can run. No real development hostname remains in the workflow file.

## Scope

### In scope

- Replace the hardcoded `ZAP_TARGET` value in `zap-coast-restitution-dev-scan.yml` with a `vars` (or secrets) reference
- Document that `ZAP_TARGET` must be configured in GitHub repository settings
- Evidence in `docs/pr-evidence.md`

### Out of scope

- Changing ZAP scan behaviour, schedule, or report upload
- Setting the live variable value in this PR (operators configure privately)
- SEC-SECRETS-001 (already shipped) or other workflows

## Journeys

1. Workflow YAML has no real hostname — `features/sec-secrets-002-zap-target-var.feature` (@R-20.1)
2. Operators know to set `ZAP_TARGET` — same feature (@R-20.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
