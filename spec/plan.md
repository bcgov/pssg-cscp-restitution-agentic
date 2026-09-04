# Plan — DEP-004 (ts-node ^10.x)

## Summary

In `restitution-app/ClientApp/package.json`, change `ts-node` from `~7.0.0` to `^10.9.0` (or equivalent `^10.x`). Regenerate/update `package-lock.json` so the resolved version is 10.x. Prefer `npm install` in ClientApp with existing legacy-peer-deps flags if the project already uses them. Append evidence noting this is a devDependency only. No production runtime change.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Target | ^10.x | Current stable major per finding |
| Scope | ClientApp only | Finding location |
| Broader upgrades | Out of scope | Keep slice tight |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
