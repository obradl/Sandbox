using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Blog.WebApi.HealthChecks
{
    public class VgHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var client = new HttpClient();
            var result = await client.GetAsync("http://vg.no", cancellationToken);
            var httpStatusCode = result.StatusCode;

            var ok = httpStatusCode == HttpStatusCode.OK;

            return ok ? new HealthCheckResult(HealthCheckStatus.Healthy, null, "VG.no is online", null) 
                : new HealthCheckResult(HealthCheckStatus.Unhealthy, null, "VG.no is offline", null);
        }

        public string Name => "VG.no";
    }
}