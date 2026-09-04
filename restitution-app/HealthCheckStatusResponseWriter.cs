#nullable enable
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Gov.Cscp.VictimServices.Public;

public static class HealthCheckStatusResponseWriter
{
    public static async Task WriteStatusOnlyAsync(HttpContext context, HealthReport report)
    {
        var overallStatus = report.Entries.TryGetValue("API", out var apiEntry) ? apiEntry.Status : report.Status;

        context.Response.StatusCode =
            overallStatus == HealthStatus.Unhealthy ? StatusCodes.Status503ServiceUnavailable : StatusCodes.Status200OK;
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(
            new { status = overallStatus.ToString() },
            new JsonSerializerOptions { WriteIndented = true }
        );

        await context.Response.WriteAsync(result);
    }
}
