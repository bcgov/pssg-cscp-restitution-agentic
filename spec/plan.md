# Plan — VULN-004 (DTO MaxLength)

## Summary

Add `[MaxLength]` to key string properties on `ParticipantDto`, `DocumentDto`, and `RestitutionApplicationDtoBase` (covers victim/offender application DTOs that inherit it). Use proportionate limits (e.g. names 100, address lines 200, email 254, phones 30, postal 20, filename 255, signature/body large but finite). Add unit tests using `Validator.TryValidateObject` proving overlong values fail. Leave ModelState behaviour in the controller unchanged.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Surface | Main submit DTOs only | Proportionate |
| Mechanism | DataAnnotations MaxLength | Already used for Required |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
