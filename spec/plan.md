# Plan — F-TEST-006 (Playwright in CI)

## Summary

Add a separate `e2e` job to `ci-restitution.yml`: checkout, setup-node, npm install ClientApp, install Playwright browsers, start SPA (`ng serve` / `npm start` with CI-friendly host/port), wait for port 4200, run `npx playwright test --project=localhost e2e/tests/health-and-routing.spec.ts`. Prefer a **separate job** so CodeQL/Trivy residuals on `gate` do not skip E2E. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Scope | health-and-routing only | No Dynamics; finding is “integrated into CI” |
| Job | Separate `e2e` job | Isolation from CodeQL gate noise |
| Remote projects | Not in CI | Out of scope |
| Browser on GHA | Prefer Chromium (drop or gate `channel: 'chrome'`) | Chrome channel often missing on ubuntu runners; fix config for CI rather than leave e2e broken |

## Residual

- Form submit E2E still manual/remote
- `gate` may still fail on known CodeQL default-setup residual; `e2e` job should still report honestly

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
