using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherApp.Models;
using WeatherApp.Models.Weather;
using WeatherApp.Services.Location;
using WeatherApp.Services.Weather;
using WeatherApp.ViewModels;

namespace UnitTestProject.ViewModels
{
    [TestClass]
    public class ItemsViewModelTests
    {

        [TestMethod]
        public void AddItemTest()
        {
            var propertiesMock = new Mock<ILocationService<LocationInfo>>();
            var weatherApiClientMock = new Mock<IWeatherApiClient>();
            LocationInfo location = new LocationInfo
            {
                Key = "6342",
                Country = new Country { EnglishName = "United States" },
                LocationName = "State College",
                PostalCode = "16802"
            };
            WeatherInfo weather = new WeatherInfo
            {
                Temperature = new Temperature { Metric = new Metric { Value = "25", Unit = "Sunny" } },
                WeatherText = "Sunny"
            };

            propertiesMock.Setup(service => service.GetItemsAsync()).ReturnsAsync(new Collection<LocationInfo>() { location });
            weatherApiClientMock.Setup(service => service.GetCurrentWeather(location.Key)).ReturnsAsync(weather);
            var itemsViewModel = new ItemsViewModel(propertiesMock.Object, weatherApiClientMock.Object);
            itemsViewModel.LoadItemsCommand.Execute(null);

            var collectionContent = itemsViewModel.LocationInfos;

            Assert.AreEqual(1, collectionContent.Count());
            location.WeatherInfo = weather;
            Assert.AreEqual(location, collectionContent.First());

        }
    }
}