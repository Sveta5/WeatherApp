using NUnit.Framework;
using System.Linq;
using UITests.PageObject;

namespace UITests.TestCases
{
    public class SmokeTets : BaseWeatherAppTest
    {
        [Test]
        public void SearchLoactionTest()
        {
            var cityName = "New York";

            var itemsPage = new ItemsPage(DriverInstance);
            Assert.IsTrue(itemsPage.IsOppened());

            var searchResults = itemsPage.ClickAddLocationButton()
                                .SetValueToSearch(cityName).ClickSearchButton()
                                .GetSearchResults();
            var firstItem = searchResults.First();

            Assert.AreEqual(cityName, firstItem);
        }

        [Test]
        public void AddAndDeleteLoactionTest()
        {
            var cityName = "New York";

            var itemsPage = new ItemsPage(DriverInstance);
            Assert.IsTrue(itemsPage.IsOppened());

            var selectedItemName = itemsPage.ClickAddLocationButton()
                                .SetValueToSearch(cityName).ClickSearchButton()
                                .SelectFirstResultItem();
            Assert.IsTrue(itemsPage.IsOppened());

            var resultList = itemsPage.WaitForValueAddedInList(selectedItemName, 5).GetLocationsList();
            Assert.IsTrue(resultList.Contains(selectedItemName));

            itemsPage.SelectLocationInList(selectedItemName).ClickDeleteLocationButton();
            resultList = itemsPage.GetLocationsList();
            Assert.IsFalse(resultList.Contains(selectedItemName));
        }
    }
}
