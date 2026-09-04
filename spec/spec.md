# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#35](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/35) — rapid assessment **VULN-003**.

## Problem

Assessment claimed the developer exception page was enabled for all non-Production environments. CONFIG-003 / LOG-001 already introduced `ExceptionPagePolicy` that allows the page only in Development. Residual risk: staging-like environment names might still be treated as Development-equivalent if the policy were loose.

## Outcome

Prove (and keep) that only Development enables the developer exception page. Staging-like names (Staging, Test, QA, UAT, Production, etc.) must not. Tighten policy only if tests show a gap.

## Scope

### In scope

- Regression tests for staging-like environment names
- Confirm Program.cs still gates via ExceptionPagePolicy
- Evidence

### Out of scope

- Changing generic error page UX
- Live Dynamics / OpenShift env naming ops beyond documenting policy

## Journeys

1. Development-only exception page — `features/vuln-003-exception-page.feature` (@R-35.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
