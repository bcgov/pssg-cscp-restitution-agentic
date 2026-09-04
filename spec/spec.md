# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#21](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/21) — rapid assessment **VULN-001**.

## Problem

Mailing address helpers concatenate user-controlled form values with raw `<br />` tags and bind them via `[innerHTML]` on the review page. Markup-like input can render as live HTML.

## Outcome

Address display on the review path is **XSS-safe**: user values are encoded or rendered as text (not unencoded `innerHTML` concatenation). Line breaks still appear. A unit test fails if script-like input is treated as live HTML.

## Scope

### In scope

- Fix `displayMailingSubAddress` (and the same-pattern `displayMailingAddress` if still used with `innerHTML`)
- Prefer encode + safe breaks, or template text bindings instead of raw `innerHTML`
- Unit test covering script-like / markup-like address input

### Out of scope

- Unrelated `[innerHTML]` usages (e.g. static alert icons) unless they take user input
- VULN-002 server file validation

## Journeys

1. No unencoded innerHTML address bind — `features/vuln-001-mailing-address-xss.feature` (@R-21.1)
2. Script-like input not live HTML — same feature (@R-21.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-04 |
| BA | Alex Rivera (simulated) | 2026-09-04 |
| QA (acceptance ownership) | | |
