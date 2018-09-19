using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blog.Infrastructure.ApiClients.WeatherClient
{
    public class WeatherClient : IWeatherClient
    {
        private readonly HttpClient _client;

        public WeatherClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://samples.openweathermap.org/data/2.5/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            _client = client;
        }

        public async Task<WeatherRoot> GetWeatherForLondon()
        {
            var data = await _client.GetAsync("weather?q=London,uk&appid=b6907d289e10d714a6e88b30761fae22");
            var jsonData = await data.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<WeatherRoot>(jsonData);
            return weatherData;
        }
    }
}
