using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ApiClients
{
    public class GitHubService
    {
        public HttpClient Client { get; }

        public GitHubService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://github.com/");
            Client = client;
        }

        public async Task<HttpStatusCode> GetFrontPage()
        {
            var response = await Client.GetAsync("/");

            return response.StatusCode;

        }
    }
}
