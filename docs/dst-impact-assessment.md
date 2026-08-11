# DST Impact Assessment — COAST Restitution Application

**Date:** 2026-08-11  
**Scope:** BC elimination of Daylight Saving Time  
**Application:** `pssg-cscp-restitution` (COAST OpenShift)  
**Branch scanned:** `development`

---

## Executive Summary

BC has legislated the elimination of Daylight Saving Time (DST), pending alignment with US federal legislation. This report documents the technical scan of the Restitution application for DST-sensitive code, containers, scheduled jobs, and integrations.

One confirmed DST issue was found and remediated. All other surfaces are DST-immune.

---

## Scan Coverage

| Surface                              | Scanned | Status                     |
| ------------------------------------ | ------- | -------------------------- |
| Angular frontend (TypeScript)        | ✅      | 1 issue remediated         |
| ASP.NET Core backend (C#)            | ✅      | No issues                  |
| Container/Dockerfile configs         | ✅      | No issues                  |
| Scheduled / batch jobs               | ✅      | No issues                  |
| OpenShift configuration              | ✅      | No issues                  |
| GitHub Actions CI cron               | ✅      | No issues                  |
| Dataverse / Dynamics 365 integration | ✅      | No issues (vendor-managed) |

---

## Findings

### FINDING 1 — REMEDIATED ✅

**Severity:** High  
**Component:** Angular frontend — outage announcement banner logic  
**File:** `restitution-app/ClientApp/src/app/store/configuration/configuration.store.ts`

#### Description

The `showAnnouncementBanner` computed signal used `moment-timezone@0.5.48` with a hardcoded `'America/Vancouver'` IANA timezone identifier to determine whether the current time falls within a configured outage window:

```typescript
// Before (DST-sensitive)
import moment from 'moment-timezone';

const current = moment().tz('America/Vancouver');
const start = moment(startDate).tz('America/Vancouver');
const end = moment(endDate).tz('America/Vancouver');
return current.isBetween(start, end, null, '[]');
```

#### DST Risk

`moment-timezone` bundles a snapshot of the IANA timezone database (tzdata) at build time. When BC eliminates DST, IANA will publish an updated tzdata release changing `America/Vancouver` to a fixed UTC offset. Until the `moment-timezone` npm package is updated and rebuilt, the bundled tzdata still reflects the old DST rules.

**Impact:** During a BC DST transition (spring-forward or fall-back) that no longer occurs, the banner could appear ~1 hour early or late relative to operator intent, if outage dates are entered as local Pacific time strings without explicit UTC offset.

#### Remediation Applied

Replaced the DST-sensitive timezone comparison with a UTC comparison using plain `moment`. The `moment-timezone` package has been removed as a dependency.

```typescript
// After (DST-immune)
import moment from 'moment';

// Compare in UTC so the outage-window check is DST-immune.
// Operators must configure CONFIGURATION_OUTAGEINFORMATION_STARTDATE /
// CONFIGURATION_OUTAGEINFORMATION_ENDDATE as UTC ISO 8601 strings
// (e.g. "2025-11-09T10:00:00Z"). When BC operates on permanent
// UTC-8 (no DST), subtract 8 h from the desired local time.
const current = moment.utc();
const start = moment.utc(startDate);
const end = moment.utc(endDate);
return current.isBetween(start, end, null, '[]');
```

**Files changed:**

| File                                                                           | Change                                                                               |
| ------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------ |
| `restitution-app/ClientApp/src/app/store/configuration/configuration.store.ts` | Switch to `moment.utc()` comparison; change import from `moment-timezone` → `moment` |
| `restitution-app/ClientApp/package.json`                                       | Remove `moment-timezone` from `dependencies`                                         |
| `restitution-app/ClientApp/angular.json`                                       | Remove `moment-timezone` from `allowedCommonJsDependencies`                          |
| `restitution-app/ClientApp/package-lock.json`                                  | Auto-updated by `npm install`                                                        |

#### Operator Action Required

Outage window environment variables must be set as **UTC ISO 8601 strings**:

```
CONFIGURATION_OUTAGEINFORMATION_STARTDATE=2025-11-09T10:00:00Z
CONFIGURATION_OUTAGEINFORMATION_ENDDATE=2025-11-09T18:00:00Z
```

When BC moves to permanent UTC-8 (no DST), a 2 am Vancouver local outage = `10:00:00Z`.  
When BC moves to permanent UTC-7 (no DST), a 2 am Vancouver local outage = `09:00:00Z`.

---

## Cleared Items (No Action Required)

### Backend API — DateTimeZoneHandling

`restitution-app/Program.cs` configures Newtonsoft.Json with:

```csharp
opts.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
```

All `DateTime` values are serialized and deserialized as UTC. Logging also uses `DateTime.UtcNow`. No DST exposure.

### Angular date-only fields

Plain `moment` (no timezone) is used in:

- `form-base.ts` — formats dates as `'MMM Do, Y'` (display only, no TZ conversion)
- `date-field.component.ts` — constructs date-only `moment` from `(year, month, day)` components

Neither path performs a timezone conversion. Not DST-sensitive.

### `new Date()` usages

- `form-base.ts`: `today = new Date()` — used to calculate 120-year boundary for birth date validation (date-only)
- `date-field.component.ts`: `new Date().getFullYear()` — year for dropdown range
- `restitution-information.helper.ts`: `let today = new Date()` — default `signatureDate` (date-only)

All are date-only operations with no time-of-day DST sensitivity.

### Container timezone

Both production Dockerfiles use Alpine Linux base images:

| Image                                         | Default TZ |
| --------------------------------------------- | ---------- |
| `mcr.microsoft.com/dotnet/aspnet:10.0-alpine` | UTC        |
| `node:22.23.1-alpine`                         | UTC        |

Neither Dockerfile sets a `TZ` environment variable. The containers run on UTC and are DST-immune.

### GitHub Actions CI cron

```yaml
- cron: '0 3 1,15 * *'
```

GitHub Actions cron syntax is evaluated in UTC. Not DST-sensitive.

### Dataverse / Dynamics 365 integration

Dataverse entity classes (`ActivityPointer`, `Email`, etc.) expose `TimeZoneRuleVersionNumber` and `UtcConversionTimeZoneCode` — standard Dynamics 365 SDK fields. Timezone conversion for CRM dates is handled server-side by the Dataverse service. Microsoft will publish updated timezone data for Dataverse when BC's DST change takes effect. No application code changes required for this layer.

---

## Residual Risks

| Risk                                                                | Likelihood | Impact                                                     | Mitigation                                                                |
| ------------------------------------------------------------------- | ---------- | ---------------------------------------------------------- | ------------------------------------------------------------------------- |
| Operator configures outage dates in local Pacific time (not UTC)    | Low        | Medium — banner shows ~1 h off                             | Document UTC requirement (this report); validate config before deployment |
| Dataverse timezone data not updated by Microsoft before BC DST date | Low        | Low — affects CRM date display only, not application logic | Monitor Microsoft Dynamics release notes                                  |
| `moment` package itself has a DST-era bug                           | Very Low   | Low                                                        | Dependabot monthly update covers this                                     |

---

## Testing Guidance

To confirm correct processing before, during, and after the BC DST transition:

1. **Outage banner — UTC format validation:**  
   Set `CONFIGURATION_OUTAGEINFORMATION_STARTDATE` / `CONFIGURATION_OUTAGEINFORMATION_ENDDATE` to UTC values bracketing "now" and verify the banner appears. Set them to past UTC values and verify it does not appear.

2. **Date-of-birth / declaration-date fields:**  
   Submit a form on a date that would have been a DST clock-change date (e.g., the second Sunday of March or first Sunday of November in Pacific time). Verify the submitted date matches the calendar date the user selected.

3. **Dataverse record dates:**  
   After BC's DST change takes effect, spot-check dates on recently created Dataverse records to confirm Dynamics displays the correct local time.

---

## References

- BC Bill 11 — Permanent Daylight Saving Time Act (2019)
- IANA Time Zone Database — `America/Vancouver` entry
- moment-timezone changelog: https://github.com/moment/moment-timezone/blob/develop/changelog.md
- Newtonsoft.Json `DateTimeZoneHandling` — https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DateTimeZoneHandling.htm
