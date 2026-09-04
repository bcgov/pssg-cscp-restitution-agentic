# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#33](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/33) — rapid assessment **LOG-004**.

## Problem

In production bootstrap, only `console.log` is suppressed. `console.error`, `console.debug`, and `console.warn` remain active and can leak diagnostic detail to end-user browser consoles.

## Outcome

In production, console log/error/debug/warn are all no-ops (or equivalently suppressed). Existing call sites remain harmless under suppression. Non-production builds keep normal console behaviour.

## Scope

### In scope

- Extend production console suppression in ClientApp `main.ts`
- Confirm/adjust call sites if needed so they remain safe under no-ops
- Light evidence (and optional unit/build check)

### Out of scope

- Removing all console calls from the codebase
- Introducing a full logging framework
- Live Dynamics

## Journeys

1. Production console suppression — `features/log-004-console-suppression.feature` (@R-33.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
