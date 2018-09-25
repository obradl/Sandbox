using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ApiClients
{
    public class GitHubService
    {
        private readonly HttpClient _client;

        public GitHubService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://github.com/");
            _client = client;
        }

        public async Task<HttpStatusCode> GetFrontPage()
        {
            var response = await _client.GetAsync("/");
            return response.StatusCode;
        }
    }
}
