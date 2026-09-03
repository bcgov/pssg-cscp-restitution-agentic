# Plan — F-TEST-005 (Angular unit substance)

## Summary

Rewrite `not-found.component.spec.ts` with a mocked `Router` (`navigateByUrl` spy). Assert constructor calls `navigateByUrl('/404')`. Remove `expect(true)` stub. Provide Router mock in TestBed. Append evidence. Do not require CI `ng test` in this slice.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Target | NotFound first | Cited in finding |
| CI Karma | Out of scope | Heavy; F-TEST-001 covered dotnet |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
