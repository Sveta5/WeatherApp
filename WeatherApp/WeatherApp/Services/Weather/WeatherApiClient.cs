using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private const string ApiKey = "apikey=eCl8zxAnkAIRifdeVX7ADI8AjpufB0A7";

        private const string LocationSearchByPostalCodeUrl = "locations/v1/cities/search?q={0}&";
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

        public async Task<IEnumerable<LocationInfo>> SearchLocation(string postalCode)
        {
            //var urlWithParametr = string.Format(LocationSearchByPostalCodeUrl, postalCode);
            //var locationInfos = await RequestResult<IEnumerable<LocationInfo>>(urlWithParametr);
            //return locationInfos;
            return new Collection<LocationInfo>
            {
                new LocationInfo
                {
                    Key = "6787_PC155"+ new Random().Next(1, 1000),
                    Country = new Country {EnglishName = "United States"},
                    LocationName = "State College",
                    PostalCode = "16802"
                }
            };
        }

        public async Task<WeatherInfo> GetCurrentWeather(string cityKey)
        {
            //var urlWithParametr = string.Format(CurretnWeatherByLocationKeyUrl, cityKey);
            //var weatherInfo = await RequestResult<WeatherInfo[]>(urlWithParametr);
            //return weatherInfo[0];
            return new WeatherInfo
            {
                Temperature = new Temperature { Metric = new Metric { Value = new Random().Next(1, 30).ToString(), Unit = "Sunny" } },
                WeatherText = "Sunny"
            };
        }
        private async Task<T> RequestResult<T>(string urlWithParametr)
        {
            Console.Write(OpenWeatherApi + urlWithParametr + ApiKey);
            var requestResult = await HttpClient.GetStringAsync(OpenWeatherApi + urlWithParametr + ApiKey);

            var resultModels = JsonConvert.DeserializeObject<T>(requestResult);
            return resultModels;
        }
    }
}