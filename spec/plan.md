# Plan — CONFIG-005 (AllowedHosts)

## Summary

Replace `AllowedHosts: "*"` in `appsettings.json` with a specific default suitable for local boot (e.g. `localhost` and related loopback names) and document that production/OpenShift hosts must be supplied via environment / config overlay (`AllowedHosts` or `ASPNETCORE_*` / env var mapping). Add a config test asserting committed base config is not `*`. Document residual risk when route hostnames differ across environments. Append evidence. Do not invent real production hostnames in committed files.

## Key decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Base config | Specific hosts, not `*` | Enables host filtering |
| Deploy hosts | Env / overlay | OpenShift hostnames stay out of repo |
| Residual | Document in evidence | Multi-host route names may need ops overlay |

## Approval (checkpoint 2)

| Role | Name | Date |
| --- | --- | --- |
| Architect / tech lead | | |
| Security (if required) | | |
