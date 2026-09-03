# Tasks — F-TEST-006

## Milestone 1

- [x] **TASK-001** — Add separate CI `e2e` job that serves ClientApp on localhost:4200 and runs Playwright `--project=localhost` for `e2e/tests/health-and-routing.spec.ts`. Covers `@R-16.1` `@R-16.2`.
- [x] **TASK-002** — Install Playwright browser deps in that job; prefer Chromium for GHA (adjust `playwright.config.ts` if `channel: 'chrome'` breaks CI); do not use remote `dev`/`test` projects. Covers `@R-16.2`.
- [x] **TASK-003** — Append `docs/pr-evidence.md`. Covers both.

## Backlog

- [ ] Full form E2E in CI — later
