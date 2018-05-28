
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using WeatherApp.Models;
using WeatherApp.Services.Weather;

namespace UnitTestProject.Services
{
    [TestClass]
    public class WeaterApiClientTests
    {
        Mock<HttpClient> httpClientMock;

        [TestInitialize]
        public void SetUpMocks()
        {
            httpClientMock = new Mock<HttpClient>();

        }
        [TestMethod]
        public void GetLocationTest()
        {
            var locationBody = JsonConvert.SerializeObject(new Collection<LocationInfo>
            {
                new LocationInfo
                {
                    Key = "6787_PC155",
                    Country = new Country {EnglishName = "United States"},
                    LocationName = "State College",
                    PostalCode = "16802"
                }
            });

            httpClientMock.Setup(service => service.GetStringAsync(It.IsRegex(".*16802.*"))).ReturnsAsync(locationBody);

            IWeatherApiClient client = new WeaterApiClient(httpClientMock.Object);

            var searchcResult = client.SearchLocation("16802");
            searchcResult.Wait();
            Assert.AreEqual(locationBody, searchcResult.Result);
        }
    }
}