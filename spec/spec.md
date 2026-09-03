# Spec — CSCP Restitution (active slice)

> Technology-free. Describe *what* and *why*. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#1](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/1) — rapid assessment **CONFIG-001** (`ra-2026-07-16T153047Z`).

## Problem

Image and dependency scans run during delivery, but **critical and high findings do not stop the pipeline**. Reports can be uploaded while a merge still deploys. Staff cannot treat a green pipeline as evidence that those scanners actually gated the change.

## Outcome

When a scanner reports **CRITICAL or HIGH** vulnerabilities (or equivalent secret findings, if that scanner is in the same job), **the job fails**. A green run means those gates passed, not that they were ignored.

## Users & personas

| Persona | Goal |
| --- | --- |
| Delivery / security reviewer | Trust that a green pipeline did not skip the vulnerability gate |
| Developer | See a failed check when CRITICAL/HIGH findings exist, so they can fix or explicitly waive |

## Scope

### In scope (this release)

- Make existing vulnerability scan steps **blocking** for CRITICAL and HIGH
- Keep SARIF (or equivalent) upload so findings remain visible in GitHub Security
- Document that scan locations may have moved since the assessment (CI vs deploy jobs)

### Out of scope

- Fixing the underlying CVEs themselves
- Changing OpenShift registry credentials or image names
- Adding a new scanner product
- Making informational/low findings fail the job
- Dataverse / application code changes

## Journeys

1. Blocking gate — see `features/config-001-trivy-gate.feature` (@R-01.1)
2. Reports still published — see same feature (@R-01.2)

## Non-functional requirements

- Accessibility: n/a (pipeline only)
- Privacy: no PII in scan logs beyond what the scanner already emits
- Availability: failing the gate may block deploy until findings are remediated or waived by a human — that is intended

## Open questions

- [x] Assessment cited `ci-restitution.yml` / `build-template.yml` with `exit-code: 0`. Current `development` has Trivy on **CD image jobs** without a failing exit code, and no Trivy on CI. This slice targets **current** scan steps, not restoring deleted workflow files.
- [ ] Existing images may already have HIGH findings; first blocking run might fail CD until remediated. Accepted for this finding.

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
