using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services.Data;
using WeatherApp.Services.Location;

namespace UnitTestProject.Services
{
    [TestClass]
    public class LocationServiceTests
    {
        Mock<IDataStore<List<LocationInfo>>> propertiesMock = new Mock<IDataStore<List<LocationInfo>>>();
        LocationInfo location = new LocationInfo
        {
            Key = "6787",
            Country = new Country { EnglishName = "United States" },
            LocationName = "State College",
            PostalCode = "16802"
        };

        [TestInitialize]
        public void SetUpMocks()
        {
            propertiesMock.Setup(service => service.StoreData(It.IsAny<List<LocationInfo>>())).ReturnsAsync(true);
            propertiesMock.Setup(service => service.GetData()).ReturnsAsync(new List<LocationInfo>());
        }

        [TestMethod]
        public void AddLocationTest()
        {
            ILocationService<LocationInfo> locatioService = new LocationService(propertiesMock.Object);

            locatioService.AddItemAsync(location).Wait();

            var itemsResult = locatioService.GetItemsAsync();
            itemsResult.Wait();

            Assert.AreEqual(1, itemsResult.Result.Count());
            Assert.AreEqual(location, itemsResult.Result.First());
        }

        [TestMethod]
        public void DeleteLocationTest()
        {
            ILocationService<LocationInfo> locatioService = new LocationService(propertiesMock.Object);

            locatioService.AddItemAsync(location).Wait();
            locatioService.DeleteItemAsync(location.Key).Wait();
            var itemsResult = locatioService.GetItemsAsync();
            itemsResult.Wait();

            Assert.AreEqual(0, itemsResult.Result.Count());
            Assert.IsFalse(itemsResult.Result.Any(item => item.Equals(location)));
        }
    }
}