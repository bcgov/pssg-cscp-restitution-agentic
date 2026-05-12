# Database Project

This project contains the Dataverse integration layer for the Restitution application (Microsoft Dynamics 365 / Dataverse).

## What it includes

- Early-bound Dataverse entity classes (`Entities/`)
- Option set enums (`OptionSets/`)
- Dataverse context and generated metadata classes
- Dataverse message contracts used by the app (`Messages/`)

## Purpose

The `Database` project provides strongly typed models and contracts so the API can read/write Dataverse records safely and consistently.

## Build

From the repo root:

```bash
dotnet build Database/Database.csproj
```

## Notes

- Most files in this project are generated from Dataverse metadata.
- Regenerate early-bound classes when Dataverse schema changes.

## XrmToolBox (quick)

Use XrmToolBox to regenerate early-bound classes when Dataverse metadata changes:

1. Run `XrmToolBox` and connect to the target Dynamics/Dataverse environment.
2. Open the early-bound generation plugin (for example, **Early Bound Generator V2**).
3. Load this project settings file: `EarlyBoundGeneratorRestitutionSettings.xml`.
4. Verify output paths point to this `Database` project.
5. Apply your changes if needed, for example, change Entities Whitelist to add or remove entities.
6. Run generation and review updated files in `Entities/`, `OptionSets/`, and related generated classes.

# Dataverse Authentication

This project supports both **on-premise (ADFS)** and **cloud (Entra ID)** authentication for Dataverse.

## Configuration

### On-Premise (ADFS) Authentication

```json
{
  "Dynamics": {
    "AuthenticationType": "OnPremise",
    "DynamicsApiEndpointUrl": "http://dev-coast-dataverse-proxy.silver.devops.bcgov/api/data/v9.0/",
    "ADFS": {
      "OAuth2TokenEndpoint": "https://ststest.gov.bc.ca/adfs/oauth2/token",
      "ClientId": "onpremise-client-id",
      "ClientSecret": "onpremise-client-secret",
      "ServiceAccountDomain": "IDIR",
      "ServiceAccountName": "onpremise-service-account-name",
      "ServiceAccountPassword": "onpremise-service-account-password",
      "ResourceName": "https://cscp-vs.dev.jag.gov.bc.ca/api/data/v9.0/"
    }
  }
}
```

### Cloud (Entra ID) Authentication

```json
{
  "Dynamics": {
    "AuthenticationType": "Cloud",
    "DynamicsApiEndpointUrl": "https://cscp-dev.api.crm3.dynamics.com/api/data/v9.2/",
    "EntraId": {
      "TenantId": "cloud-tenant-id",
      "ClientId": "cloud-client-id",
      "ClientSecret": "cloud-client-secret",
      "ResourceName": "https://cscp-dev.api.crm3.dynamics.com"
    }
  }
}
```
