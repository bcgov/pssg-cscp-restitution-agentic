using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using Database.Extensions;
using Gov.Cscp.VictimServices.Public.HealthChecks;
using Gov.Cscp.VictimServices.Public.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using NWebsec.AspNetCore.Mvc;
using NWebsec.AspNetCore.Mvc.Csp;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Configuration
// ---------------------------------------------------------------------------
builder
    .Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true
    )
    .AddEnvironmentVariables();

// ---------------------------------------------------------------------------
// Logging (Serilog)
// ---------------------------------------------------------------------------
ConfigureLogging(builder.Environment, builder.Configuration);
builder.Host.UseSerilog();

// ---------------------------------------------------------------------------
// Services
// ---------------------------------------------------------------------------
var services = builder.Services;

// Dataverse / Database
services.AddDatabase(builder.Configuration);

// Application services
services.AddScoped<ILookupQueryService, LookupQueryService>();

// Routing
services.AddRouting(options => options.LowercaseUrls = true);

// MVC + security headers (NWebsec filters)
services
    .AddControllers(opts =>
    {
        opts.Filters.Add(typeof(NoCacheHttpHeadersAttribute));
        opts.Filters.Add(new XRobotsTagAttribute() { NoIndex = true, NoFollow = true });
        opts.Filters.Add(typeof(XContentTypeOptionsAttribute));
        opts.Filters.Add(typeof(XDownloadOptionsAttribute));
        opts.Filters.Add(typeof(XFrameOptionsAttribute));
        opts.Filters.Add(typeof(XXssProtectionAttribute));
        opts.Filters.Add(typeof(CspReportOnlyAttribute));
        opts.Filters.Add(new CspScriptSrcReportOnlyAttribute { None = true });
    })
    .AddNewtonsoftJson(opts =>
    {
        opts.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        opts.SerializerSettings.DateFormatHandling = Newtonsoft
            .Json
            .DateFormatHandling
            .IsoDateFormat;
        opts.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

        // Prevent JSON parser issues with circular references in user / roles model.
        opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json
            .ReferenceLoopHandling
            .Ignore;
    });

// Data Protection key ring persistence (for container deployments)
if (!string.IsNullOrEmpty(builder.Configuration["KEY_RING_DIRECTORY"]))
{
    services
        .AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["KEY_RING_DIRECTORY"]!));
}

// Allow large file uploads (1 GB)
services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1073741824;
});

// Health checks
services
    .AddHealthChecks()
    .AddCheck<ApiSelfHealthCheck>(
        "API",
        failureStatus: HealthStatus.Degraded,
        tags: new[] { "self", "process" }
    )
    .AddCheck<DataverseHealthCheck>(
        "Dataverse",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "dataverse", "dynamics", "ready" }
    );

// Swagger / OpenAPI
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.OpenApiInfo
        {
            Title = "Restitution API",
            Version = "v1",
            Description = "API for the Restitution Application",
        }
    );
});

// ---------------------------------------------------------------------------
// Build
// ---------------------------------------------------------------------------
var app = builder.Build();

// ---------------------------------------------------------------------------
// Middleware pipeline
// ---------------------------------------------------------------------------
string pathBase = builder.Configuration["BASE_PATH"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSerilogRequestLogging(options =>
{
    options.GetLevel = (httpContext, elapsed, ex) =>
    {
        if (ex != null)
            return Serilog.Events.LogEventLevel.Error;

        var path = httpContext.Request.Path.ToString();

        if (path.StartsWith("/hc", StringComparison.OrdinalIgnoreCase))
            return httpContext.Response.StatusCode >= 500
                ? Serilog.Events.LogEventLevel.Error
                : Serilog.Events.LogEventLevel.Verbose;

        if (path.StartsWith("/api/lookup", StringComparison.OrdinalIgnoreCase))
            return Serilog.Events.LogEventLevel.Verbose;

        return elapsed > 1000
            ? Serilog.Events.LogEventLevel.Warning
            : Serilog.Events.LogEventLevel.Information;
    };

    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
    };
});

// Health check – returns JSON with API + Dataverse status.
// Overall status (and HTTP status code) is driven solely by the API self-check so that
// a Dataverse outage does not cause a pod crash-restart loop.
app.MapHealthChecks(
    "/hc",
    new HealthCheckOptions
    {
        // Allow the endpoint to be reached even when the aggregate status is Unhealthy.
        ResultStatusCodes =
        {
            [HealthStatus.Healthy] = 200,
            [HealthStatus.Degraded] = 200,
            [HealthStatus.Unhealthy] = 200,
        },
        ResponseWriter = async (context, report) =>
        {
            // Determine overall status from the API self-check only.
            // Dataverse failures are surfaced in the per-check details but do not
            // flip the overall status to Unhealthy (which would restart the pod).
            var overallStatus = report.Entries.TryGetValue("API", out var apiEntry)
                ? apiEntry.Status
                : report.Status;

            context.Response.StatusCode = overallStatus == HealthStatus.Unhealthy ? 503 : 200;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(
                new
                {
                    status = overallStatus.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                    }),
                },
                new JsonSerializerOptions { WriteIndented = true }
            );

            await context.Response.WriteAsync(result);
        },
    }
);

// Swagger (development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restitution API v1");
        c.RoutePrefix = "swagger";
    });
}

// Security headers
app.Use(
    async (ctx, next) =>
    {
        ctx.Response.Headers.Append(
            "Content-Security-Policy",
            "script-src 'self' 'unsafe-eval' 'unsafe-inline' https://apis.google.com https://maxcdn.bootstrapcdn.com https://cdnjs.cloudflare.com https://code.jquery.com https://stackpath.bootstrapcdn.com https://fonts.googleapis.com"
        );
        ctx.Response.Headers.Append(
            "Strict-Transport-Security",
            "max-age=31536000; includeSubDomains; preload"
        );
        await next();
    }
);

app.UseXContentTypeOptions();
app.UseReferrerPolicy(opts => opts.NoReferrer());
app.UseXfo(xfo => xfo.Deny());

if (!app.Environment.IsDevelopment())
{
    app.UseCsp(opts =>
    {
        opts.BlockAllMixedContent()
            .StyleSources(s =>
                s.Self()
                    .UnsafeInline()
                    .CustomSources(
                        "https://use.fontawesome.com",
                        "https://stackpath.bootstrapcdn.com",
                        "https://fonts.googleapis.com"
                    )
            )
            .FontSources(s =>
                s.Self().CustomSources("https://use.fontawesome.com", "https://fonts.gstatic.com")
            )
            .FormActions(s => s.Self())
            .FrameAncestors(s => s.Self())
            .ImageSources(s => s.Self().CustomSources("data:"))
            .DefaultSources(s => s.Self())
            .ObjectSources(s => s.Self().CustomSources("data:"))
            .FrameSources(s => s.Self().CustomSources("data:"))
            .ScriptSources(s =>
                s.Self()
                    .CustomSources(
                        "https://apis.google.com",
                        "https://maxcdn.bootstrapcdn.com",
                        "https://cdnjs.cloudflare.com",
                        "https://code.jquery.com",
                        "https://stackpath.bootstrapcdn.com",
                        "https://fonts.googleapis.com"
                    )
            );
    });
}

app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseNoCacheHttpHeaders();

app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always,
        MinimumSameSitePolicy = SameSiteMode.None,
    }
);

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();

// ---------------------------------------------------------------------------
// Serilog configuration
// ---------------------------------------------------------------------------
static void ConfigureLogging(IHostEnvironment env, IConfiguration configuration)
{
    var loggerConfiguration = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("app", "Restitution")
        .Enrich.WithProperty("environment", env.EnvironmentName)
        .Enrich.WithEnvironmentUserName()
        .Enrich.WithCorrelationId()
        .Enrich.WithSpan()
        .Enrich.WithProperty(
            "version",
            Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown"
        )
        .Enrich.WithProperty("UTC_Timestamp", DateTime.UtcNow.ToString("o"));

    if (env.IsDevelopment())
    {
        loggerConfiguration.MinimumLevel.Debug();
    }
    else
    {
        loggerConfiguration.MinimumLevel.Information();
    }

    loggerConfiguration
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning);

    loggerConfiguration.WriteTo.Console();

    var splunkCollectorUrl = configuration["SPLUNK_COLLECTOR_URL"];
    var splunkToken = configuration["SPLUNK_TOKEN"];

    if (!string.IsNullOrEmpty(splunkCollectorUrl) && !string.IsNullOrEmpty(splunkToken))
    {
        HttpClientHandler handler = null;

        if (env.IsDevelopment())
        {
            handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };
        }

        loggerConfiguration.WriteTo.EventCollector(
            splunkHost: splunkCollectorUrl,
            eventCollectorToken: splunkToken,
            sourceType: "coast:restitution:api",
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
            messageHandler: handler,
            batchSizeLimit: 100,
            batchIntervalInSeconds: 2
        );
    }

    Log.Logger = loggerConfiguration.CreateLogger();

    Serilog.Debugging.SelfLog.Enable(msg =>
    {
        Console.Error.WriteLine($"Serilog Error: {msg}");
    });

    Log.Logger.Information("Restitution API Started");
}
