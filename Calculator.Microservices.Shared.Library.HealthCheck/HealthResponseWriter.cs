using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace Calculator.Microservices.Shared.Library.HealthCheck
{
    public class HealthResponseWriter
    {
        const string DEFAULT_CONTENT_TYPE = "application/json";

        private static byte[] emptyResponse = new byte[] { (byte)'{', (byte)'}' };

        public static async Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
        {
            if (report != null)
            {
                httpContext.Response.ContentType = DEFAULT_CONTENT_TYPE;

                var uiReport = UIHealthReport.CreateFrom(report);

                using var responseStream = new MemoryStream();

                await JsonSerializer.SerializeAsync(responseStream, uiReport);
                await httpContext.Response.Body.WriteAsync(responseStream.ToArray());
            }
            else
            {
                await httpContext.Response.Body.WriteAsync(emptyResponse);
            }
        }
    }
}

