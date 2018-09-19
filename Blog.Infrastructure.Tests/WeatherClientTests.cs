using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blog.Infrastructure.ApiClients.WeatherClient;
using Xunit;

namespace Blog.Infrastructure.Tests
{
    public class WeatherClientTests
    {
        [Fact]
        public async Task GetWeatherDataNotNull()
        {
            var client = new WeatherClient(new HttpClient(new HttpClientHandler()));
            var data = await client.GetWeatherForLondon();
            Assert.NotNull(data);
        }
    }
}
