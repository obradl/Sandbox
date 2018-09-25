using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Blog.Infrastructure.ApiClients;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Blog.WebApi.HealthChecks
{
    public class GitHubHealthCheck : IHealthCheck
    {
        private readonly GitHubService _gitHubService;

        public GitHubHealthCheck(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _gitHubService.GetFrontPage();
            var ok = result == HttpStatusCode.OK;

            return ok ? new HealthCheckResult(HealthCheckStatus.Healthy, null, "GitHb is online", null) 
                : new HealthCheckResult(HealthCheckStatus.Unhealthy, null, "GitHb is offline", null);
        }

        public string Name => "GitHubHealthCheck";
    }
}