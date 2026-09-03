# Plan — CONFIG-003 (dev exception page)

## Summary

In `restitution-app/Program.cs`, change `if (!app.Environment.IsProduction())` around `UseDeveloperExceptionPage` to `if (app.Environment.IsDevelopment())`, keep `else UseExceptionHandler("/Home/Error")`. Append evidence. No new host tests required unless easy.

## Architecture

```text
Development → UseDeveloperExceptionPage
else → UseExceptionHandler("/Home/Error")
```

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Gate | IsDevelopment only | Matches finding; Staging/Test safe |
| Related issues | Leave LOG-001 / VULN-003 open | Separate tickets; may close later as dup |

## Test approach

- Diff review; `@R-08.1` `@R-08.2`
- Append `docs/pr-evidence.md`

## Rollout

- Merge to `development`

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | Sam Okonkwo (simulated) | 2026-09-03 |
| Security (if required) | | |
