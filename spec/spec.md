# Spec — CSCP Restitution (active slice)

> Technology-free. Active slice only; prior slices remain in git history and `spec/features/`.

## Upstream intent

GitHub issue [#31](https://github.com/bcgov/pssg-cscp-restitution-agentic/issues/31) — rapid assessment **DEP-005**.

## Problem

ClientApp depends on moment.js (maintenance-mode / legacy) and `@angular/material-moment-adapter`, which pulls moment into the runtime bundle. Direct call sites are limited (date-field display/compare and form-base date formatting); Material date pickers are wired through MomentDateAdapter. No known CVEs at assessment time, but the library is no longer actively developed and inflates payload.

## Outcome

Applicant date entry and display continue to work using native `Date` / `Intl` (or an equivalent maintained adapter). Direct moment imports are gone; Material date adapters no longer require moment. The `moment` and `@angular/material-moment-adapter` packages are removed from ClientApp dependencies when unused. No live Dynamics or broader UI rewrite.

## Scope

### In scope

- Replace moment usage in `date-field` and `form-base` date formatting/compare with native `Date` / `Intl`
- Switch Material `DateAdapter` providers from MomentDateAdapter to Angular native DateAdapter (adjust formats as needed for native)
- Remove unused `moment` and `@angular/material-moment-adapter` (and related CommonJS allowlist) when no longer referenced
- Light unit coverage and evidence

### Out of scope

- Rewriting the entire Angular app or unrelated date UX redesign
- Adding dayjs/luxon/date-fns unless native proves insufficient for the two call sites + Material adapter
- Live Dynamics / OpenShift / broader npm major upgrades

## Journeys

1. Moment removed from ClientApp runtime — `features/dep-005-moment.feature` (@R-31.1, @R-31.2)

## Sign-off (checkpoint 1)

| Role | Name | Date |
| --- | --- | --- |
| Product / PM | | |
| BA | | |
| QA (acceptance ownership) | | |
