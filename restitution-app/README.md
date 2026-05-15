# Restitution API

ASP.NET Core API for the Restitution application.

## Requirements

- .NET SDK 10

## Local development

From `restitution-app`:

```bash
dotnet restore
dotnet run
```

Default local URL (from launch settings): `http://localhost:5000`

## User secrets

[Setup secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows#secret-manager)

### Secrets Template

```JSON
{
  "BASE_PATH": "/restwebforms",

  "FEATURE_UPDATED_COMPLIANCE_FIELDS": "true",

  "Dynamics": {
    "AuthenticationType": "OnPremise",
    "ADFS": {
      "DynamicsApiEndpointUrl": "http://dev-coast-dataverse-proxy.silver.devops.bcgov/api/data/v9.0/",
      "OAuth2TokenEndpoint": "https://ststest.gov.bc.ca/adfs/oauth2/token",
      "ClientId": "<onpremise_client_id>",
      "ClientSecret": "<onpremise_client_secret>",
      "ServiceAccountName": "<onpremise_service_account_username>",
      "ServiceAccountPassword": "<onpremise_service_account_password>",
      "ResourceName": "https://cscp-vs.dev.jag.gov.bc.ca/api/data/v9.0/"
    },
    "EntraId": {
      "DynamicsApiEndpointUrl": "https://csvs-coast-dev.api.crm3.dynamics.com/api/data/v9.2/",
      "TenantId": "<cloud_tenant_id>",
      "ClientId": "<cloud_client_id>",
      "ClientSecret": "<cloud_client_secret>",
      "ResourceName": "https://csvs-coast-dev.crm3.dynamics.com"
    }
  }
}
```

## Swagger

- UI: `http://localhost:5000/swagger`
- OpenAPI JSON: `http://localhost:5000/swagger/v1/swagger.json`
