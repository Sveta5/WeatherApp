using System.Collections.ObjectModel;
using System.Linq;
using Moq;
using NUnit.Framework;
using WeatherApp.Models;
using WeatherApp.Services.Location;
using WeatherApp.Services.Weather;
using WeatherApp.ViewModels;

namespace UnitTestProject.ViewModels
{
    [TestFixture]
    public class SearchViewModelTests
    {
        [Test]
        public void SearchWeatherTets()
        {
            var propertiesMock = new Mock<ILocationService<LocationInfo>>();
            var weatherApiClientMock = new Mock<IWeatherApiClient>();
            LocationInfo location = new LocationInfo
            {
                Key = "6342",
                Country = new Country {EnglishName = "United States"},
                LocationName = "State College",
                PostalCode = "16802"
            };
            weatherApiClientMock.Setup(service => service.SearchLocation(location.PostalCode)).ReturnsAsync(new Collection<LocationInfo>(){location});
            var searchViewModel = new SearchViewModel(propertiesMock.Object, weatherApiClientMock.Object);
            searchViewModel.SearchForLocation(location.PostalCode).Wait();

            var collectionContent = searchViewModel.LocationInfos;

            Assert.AreEqual(1, collectionContent.Count());
            Assert.AreEqual(location, collectionContent.First());
        }
    }
}