using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Gov.Cscp.VictimServices.Public.Services;
using Gov.Cscp.VictimServices.Public.Utilities.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NWebsec.AspNetCore.Mvc;
using NWebsec.AspNetCore.Mvc.Csp;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

namespace Gov.Cscp.VictimServices.Public
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<TokenHandler>();

            services.AddHttpClient<ICOASTAuthService, COASTAuthService>();
            services
                .AddHttpClient<IDynamicsResultService, DynamicsResultService>()
                .AddHttpMessageHandler<TokenHandler>();

            services.AddMemoryCache();

            // for security reasons, the following headers are set.
            services
                .AddMvc(opts =>
                {
                    opts.EnableEndpointRouting = false;
                    // default deny
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    opts.Filters.Add(new AuthorizeFilter(policy));

                    opts.Filters.Add(typeof(NoCacheHttpHeadersAttribute));
                    opts.Filters.Add(new XRobotsTagAttribute() { NoIndex = true, NoFollow = true });
                    opts.Filters.Add(typeof(XContentTypeOptionsAttribute));
                    opts.Filters.Add(typeof(XDownloadOptionsAttribute));
                    opts.Filters.Add(typeof(XFrameOptionsAttribute));
                    opts.Filters.Add(typeof(XXssProtectionAttribute));
                    //CSPReportOnly
                    opts.Filters.Add(typeof(CspReportOnlyAttribute));
                    opts.Filters.Add(new CspScriptSrcReportOnlyAttribute { None = true });

                    opts.Filters.Add(new AllowAnonymousFilter()); // Allow anonymous for dev
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    opts.SerializerSettings.DateFormatHandling = Newtonsoft
                        .Json
                        .DateFormatHandling
                        .IsoDateFormat;
                    opts.SerializerSettings.DateTimeZoneHandling = Newtonsoft
                        .Json
                        .DateTimeZoneHandling
                        .Utc;

                    // ReferenceLoopHandling is set to Ignore to prevent JSON parser issues with the user / roles model.
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                        .Json
                        .ReferenceLoopHandling
                        .Ignore;

                    // avoid converting whole incoming model, it may lead to NullValueHandling.Ignore removing expected properties
                    // use coverter on individual properties instead, for example int? type + optionset combo
                    opts.SerializerSettings.Converters.Add(new EmptyStringToNullConverter());
                });

            // services.RegisterPermissionHandler();

            // setup key ring to persist in storage.
            if (!string.IsNullOrEmpty(Configuration["KEY_RING_DIRECTORY"]))
            {
                services
                    .AddDataProtection()
                    .PersistKeysToFileSystem(
                        new DirectoryInfo(Configuration["KEY_RING_DIRECTORY"])
                    );
            }

            // allow for large files to be uploaded
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1073741824; // 1 GB
            });

            // health checks
            services
                .AddHealthChecks()
                .AddCheck("HTTP Endpoint", () => HealthCheckResult.Healthy("Ok"));

            services.AddSession();

            // Add Swagger/OpenAPI
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory
        )
        {
            var log = loggerFactory.CreateLogger("Startup");

            string pathBase = Configuration["BASE_PATH"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // health checks
            app.UseHealthChecks("/hc");

            // Enable Swagger and Swagger UI only in development
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restitution API v1");
                    c.RoutePrefix = "swagger";
                });
            }

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

            if (!env.IsDevelopment()) // when running locally we can't have a strict CSP
            {
                // Content-Security-Policy header
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
                            s.Self()
                                .CustomSources(
                                    "https://use.fontawesome.com",
                                    "https://fonts.gstatic.com"
                                )
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
            // IMPORTANT: This session call MUST go before UseMvc()
            app.UseSession();

            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    HttpOnly = HttpOnlyPolicy.Always,
                    Secure = CookieSecurePolicy.Always,
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None,
                }
            );

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}");
            });

            if (
                !string.IsNullOrEmpty(Configuration["SPLUNK_COLLECTOR_URL"])
                && !string.IsNullOrEmpty(Configuration["SPLUNK_TOKEN"])
            )
            {
                Serilog.Sinks.Splunk.CustomFields fields = new Serilog.Sinks.Splunk.CustomFields();
                if (!string.IsNullOrEmpty(Configuration["SPLUNK_CHANNEL"]))
                {
                    fields.CustomFieldList.Add(
                        new Serilog.Sinks.Splunk.CustomField(
                            "channel",
                            Configuration["SPLUNK_CHANNEL"]
                        )
                    );
                }
                var splunkUri = new Uri(Configuration["SPLUNK_COLLECTOR_URL"]);
                var upperSplunkHost = splunkUri.Host?.ToUpperInvariant() ?? string.Empty;

                // Fix for bad SSL issues
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithCorrelationId()
                    .Enrich.WithCorrelationIdHeader()
                    .Enrich.WithSpan()
                    .Enrich.WithProperty(
                        "version",
                        Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown"
                    )
                    .Enrich.WithProperty("UTC_Timestamp", DateTime.UtcNow.ToString("o"))
                    .WriteTo.EventCollector(
                        splunkHost: Configuration["SPLUNK_COLLECTOR_URL"],
                        eventCollectorToken: Configuration["SPLUNK_TOKEN"],
                        sourceType: "restitution",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
#pragma warning disable CA2000 // Dispose objects before losing scope
                        messageHandler: new HttpClientHandler()
                        {
                            ServerCertificateCustomValidationCallback = (
                                message,
                                cert,
                                chain,
                                errors
                            ) =>
                            {
                                return true;
                            },
                        }
#pragma warning restore CA2000 // Dispose objects before losing scope
                    )
                    .WriteTo.Console()
                    .CreateLogger();

                Serilog.Debugging.SelfLog.Enable(Console.Error);

                Log.Logger.Information("Restitution Webforms Started");
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                    .CreateLogger();
            }
        }
    }
}
