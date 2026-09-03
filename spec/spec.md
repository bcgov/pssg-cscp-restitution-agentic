# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#17](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/17) — rapid assessment **LOG-001**.

## Problem

Developer exception pages expose stack traces outside Production. **CONFIG-003** already changed the gate to Development-only; LOG-001 is the logging/disclosure twin of that finding and remains open as its own ticket.

## Outcome

A **regression guard** locks the Development-only rule: shared Staging/Test must not register the developer exception page. Behaviour matches CONFIG-003; this slice adds an explicit testable helper/assertion and evidence so LOG-001 can close.

## Scope

### In scope

- Keep developer exception page only when `IsDevelopment()` (already true — do not regress)
- Extract a small testable helper (or equivalent) and unit-test Development vs Staging/Production
- Evidence links CONFIG-003; note VULN-003 may still be open as duplicate hygiene

### Out of scope

- Changing `/Home/Error` UI
- Splunk sinks
- VULN-003 ticket close (separate unless product asks)

## Journeys

1. Helper/test covers environments — `features/log-001-dev-exception-disclosure.feature` (@R-17.1)
2. Staging excluded — same feature (@R-17.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | Alex Rivera (simulated) | 2026-09-03 |
| BA | Alex Rivera (simulated) | 2026-09-03 |
| QA (acceptance ownership) | | |
