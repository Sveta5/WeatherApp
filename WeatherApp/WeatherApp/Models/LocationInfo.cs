using Newtonsoft.Json;
using System.Collections.Generic;
using WeatherApp.Models.Weather;

namespace WeatherApp.Models
{
    public class LocationInfo
    {
        public string Key { get; set; }
        [JsonProperty("PrimaryPostalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("EnglishName")]
        public string LocationName { get; set; }
        public Country Country { get; set; }
        [JsonIgnore]
        public WeatherInfo WeatherInfo { get; set; }
    }
}
