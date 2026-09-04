# Plan — VULN-003 (Development-only exception page)

## Summary

Policy already uses `environment.IsDevelopment()` only. Expand unit tests to cover additional staging-like names (QA, UAT, PreProduction, Production) and add a lightweight source assertion that `Program.cs` calls `ExceptionPagePolicy.AllowDeveloperExceptionPage` before `UseDeveloperExceptionPage`. No policy change unless a gap appears.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Policy | Keep Development-only | Already correct |
| Proof | Expand regression tests | Twin of CONFIG-003/LOG-001 |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
