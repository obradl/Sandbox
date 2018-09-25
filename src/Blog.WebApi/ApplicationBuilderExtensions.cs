using System.Linq;
using System.Net.Mime;
using Blog.WebApi.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Blog.WebApi
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app, string healthCheckUrl)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = async (context, result) =>
                {
                    var healthCheckResults = new HealthCheckResultsDto { OverallStatus = result.Status.ToString() };

                    var healthChecks = result.Results
                        .Select(r => new HealthCheckDto { Name = r.Key, Status = r.Value.Status.ToString() }).ToList();

                    healthCheckResults.HealthChecks = healthChecks;
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = new ContentType("application/json").MediaType;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(healthCheckResults));
                }
            });

            return app;
        }
    }
}