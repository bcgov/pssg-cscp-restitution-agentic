# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#30](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/30) — rapid assessment **DEP-004**.

## Problem

ClientApp pins `ts-node` to ~7.0.0 (resolves 7.0.1, 2018). Current stable is 10.x. It is a development-only dependency used to run TypeScript config (e.g. Playwright/Orval), but outdated tooling carries supply-chain risk in CI/build.

## Outcome

Bump `ts-node` to a current major (`^10.x`) and refresh the lockfile so installs resolve a supported 10.x release. Confirm build/test tooling that invokes ts-node still works (or document no breakage). No production runtime change.

## Scope

### In scope

- Bump `ts-node` in ClientApp `package.json` to `^10.x`
- Update `package-lock.json`
- Light verification that configs still load / CI npm steps succeed
- Evidence

### Out of scope

- Broader Angular/npm major upgrades
- Production runtime dependency changes
- Live Dynamics

## Journeys

1. ts-node on supported major — `features/dep-004-ts-node.feature` (@R-30.1)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
