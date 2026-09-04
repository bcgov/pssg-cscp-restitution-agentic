# Plan — DEP-005 (remove moment.js)

## Summary

Migrate ClientApp date handling off moment.js without rewriting the Angular app.

1. In `date-field.component.ts`, replace moment construction/compare with native `Date` (`getFullYear` / `getMonth` / `getDate`, `<` / `>` vs min/max). Patch form controls with `Date` (not moment).
2. In `form-base.ts` `datesOrEmpty`, format with `Intl.DateTimeFormat` (or equivalent) approximating the prior “MMM Do, Y” style for review display.
3. Replace `MomentDateAdapter` providers with Angular Material `NativeDateAdapter` in restitution-application and the four restitution shared components that currently provide MomentDateAdapter. Adjust `MY_FORMATS` so native adapter display tokens work (or use `MAT_NATIVE_DATE_FORMATS` if formats are incompatible).
4. Remove `moment` and `@angular/material-moment-adapter` from `package.json`, refresh lockfile, drop `moment` from `angular.json` `allowedCommonJsDependencies`.
5. Add/extend a small unit test for date formatting or date-field bounds; append `docs/pr-evidence.md`.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Replacement | Native `Date` / `Intl` + `NativeDateAdapter` | Avoid new date library; Material ships native adapter |
| dayjs/luxon | Out unless blocked | Spec prefers native first |
| Blast radius | Date providers + two call sites + package removal | Proportionate to finding |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
