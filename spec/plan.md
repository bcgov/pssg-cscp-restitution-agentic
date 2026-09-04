# Plan — AUTH-002 (SameSite Lax)

## Summary

In `Program.cs` cookie policy, change `MinimumSameSitePolicy = SameSiteMode.None` to `SameSiteMode.Lax`. Prefer extracting a tiny testable constant/helper if that makes the unit assertion cleaner (optional). Add a unit/config test that fails if the policy is `None`. Append evidence. No documented reason found to keep `None` for this public restitution form app.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Policy | `SameSiteMode.Lax` | Finding preference; no cross-site cookie need documented |
| Test | Assert configured policy ≠ None / == Lax | CI without live browser |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-04 |
| Security (if required) | | |
