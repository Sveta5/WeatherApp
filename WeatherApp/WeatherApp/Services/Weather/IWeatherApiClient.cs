using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.Weather;

namespace WeatherApp.Services.Weather
{
    public interface IWeatherApiClient
    {
        Task<IEnumerable<LocationInfo>> SearchLocation(string searchValue);
        Task<WeatherInfo> GetCurrentWeather(string cityKey);
    }
}