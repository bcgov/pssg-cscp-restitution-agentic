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

## Tests

Unit tests live in the sibling `restitution-app.Tests` project, which is part of `restitution-app.sln`. They run offline and do not need Dataverse credentials.

```bash
dotnet test restitution-app.sln
```

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

The on-premise ADFS application must be configured to allow the client-credentials grant.

## Data Protection key ring (container deployments)

When `KEY_RING_DIRECTORY` is set, the ASP.NET Core Data Protection key ring is persisted to that
directory instead of the in-memory default. Because Windows DPAPI is unavailable on Linux/OpenShift,
the persisted keys must also be protected at rest with an X.509 certificate:

| Variable | Required | Description |
| --- | --- | --- |
| `KEY_RING_DIRECTORY` | When enabling filesystem persistence | Directory (e.g. a mounted volume) the key ring XML is written to. |
| `KEY_RING_CERTIFICATE_PATH` | Yes, once `KEY_RING_DIRECTORY` is set (except local Development) | Path to a certificate file (`.pfx`/`.p12`, or a public-only `.cer`/`.pem`) used to encrypt the key ring at rest. |
| `KEY_RING_CERTIFICATE_PASSWORD` | Only if the certificate file is password-protected | Password for the `.pfx`/`.p12` file. Set via secrets/environment only — never commit it. |

If `KEY_RING_DIRECTORY` is set but no usable certificate can be loaded from
`KEY_RING_CERTIFICATE_PATH`, the application fails to start in any environment other than
Development, to avoid silently persisting plaintext key material. Local Development without
`KEY_RING_DIRECTORY` set is unaffected (Data Protection keys remain ephemeral/in-memory).

## Swagger

- UI: `http://localhost:5000/swagger`
- OpenAPI JSON: `http://localhost:5000/swagger/v1/swagger.json`
