# Plan — VULN-002 (server file extension validation)

## Summary

Add a small server-side helper that checks each `DocumentDto.Filename` extension against the same allowlist as `ClientApp` `config.ts` (`pdf`, `png`, `jpeg`, `jpg`, `doc`, `docx`, `ppt`). Call it from `RestitutionsController.SubmitRestitutionInternal` (or shared submit entry) after model-state validation and **before** Dynamics `ExecuteAsync`. On failure return **400**. Unit-test the helper (and optionally controller with a fake org service) without live Dynamics. Append evidence.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Check | Filename extension only | Matches finding; MIME sniff out of scope |
| Allowlist | Mirror client config.ts | Avoid client/server drift for this slice |
| Placement | Shared helper + submit path | Testable without Dynamics |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-04 |
| Security (if required) | | |
