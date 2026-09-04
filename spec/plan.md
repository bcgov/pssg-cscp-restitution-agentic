# Plan — LOG-004 (console suppression)

## Summary

In production `main.ts`, assign no-op functions to `console.log`, `console.error`, `console.debug`, and `console.warn` (same pattern as existing log suppression). Leave existing call sites as-is — they become harmless under no-ops. Append evidence. Optional: tiny comment noting production-only.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Scope | main.ts production block | Finding location |
| Call sites | Leave; suppress | Proportionate |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
