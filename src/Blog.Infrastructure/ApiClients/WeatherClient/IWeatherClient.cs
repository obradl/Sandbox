using System.Threading.Tasks;

namespace Blog.Infrastructure.ApiClients.WeatherClient
{
    public interface IWeatherClient
    {
        Task<WeatherRoot> GetWeatherForLondon();
    }
}