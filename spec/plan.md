# Plan — LOG-001 (exception page disclosure guard)

## Summary

Add `ExceptionPagePolicy.ShouldUseDeveloperExceptionPage(IHostEnvironment)` (or static env-name helper) returning `env.IsDevelopment()`. Use it in `Program.cs` instead of inline check. Unit-test Development true / Staging & Production false. Append evidence citing CONFIG-003.

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
