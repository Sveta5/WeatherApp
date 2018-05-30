using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;
using WeatherApp.Models.Weather;

namespace WeatherApp.Services.Weather
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private const string OpenWeatherApi = "http://dataservice.accuweather.com/";
        private const string ApiKey = "apikey=jUIuk9wNsyLBFl1cpP1nEmFCEFqMsP5l";//"apikey=eCl8zxAnkAIRifdeVX7ADI8AjpufB0A7";

        private const string LocationSearchByValueUrl = "locations/v1/cities/search?q={0}&";
        private const string CurretnWeatherByLocationKeyUrl = "currentconditions/v1/{0}?";

        private readonly HttpClient HttpClient;

        public WeatherApiClient()
        {
            HttpClient = new HttpClient();
        }
        public WeatherApiClient(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public async Task<IEnumerable<LocationInfo>> SearchLocation(string searchValue)
        {
            var urlWithParametr = string.Format(LocationSearchByValueUrl, searchValue);
            var locationInfos = await RequestResult<IEnumerable<LocationInfo>>(urlWithParametr);
            return locationInfos;
        }

        public async Task<WeatherInfo> GetCurrentWeather(string cityKey)
        {
            var urlWithParametr = string.Format(CurretnWeatherByLocationKeyUrl, cityKey);
            var weatherInfo = await RequestResult<WeatherInfo[]>(urlWithParametr);
            return weatherInfo?[0];
        }

        private async Task<T> RequestResult<T>(string urlWithParametr)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(OpenWeatherApi + urlWithParametr + ApiKey);
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.Message);
                return default(T);
            }
            if (!response.IsSuccessStatusCode) return default(T);

            var responseBody = await response.Content.ReadAsStringAsync();
            var resultModels = JsonConvert.DeserializeObject<T>(responseBody);

            return resultModels;
        }
    }
}