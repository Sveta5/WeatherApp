using NUnit.Framework;
using System.Linq;
using UITests.PageObject;

namespace UITests.TestCases
{
    public class SmokeTets : BaseWeatherAppTest
    {
        [Test]
        public void SearchLoactionByLocationNameTest()
        {
            var cityName = "New York";

            var itemsPage = new ItemsPage(DriverInstance);
            Assert.IsTrue(itemsPage.IsOppened());

            var firstItem = itemsPage.ClickAddLocationButton()
                                .SetValueToSearch(cityName).ClickSearchButton()
                                .GetSearchResultLocationNames().First();

            Assert.AreEqual(cityName, firstItem);
        }

        [Test]
        public void SearchLoactionByPostalCodeTest()
        {
            var postalCode = "16801";

            var itemsPage = new ItemsPage(DriverInstance);
            Assert.IsTrue(itemsPage.IsOppened());

            var firstItem = itemsPage.ClickAddLocationButton()
                                .SetValueToSearch(postalCode).ClickSearchButton()
                                .GetSearchResultPostalCodes().First();

            Assert.AreEqual(postalCode, firstItem);
        }

        [Test]
        public void AddAndDeleteLoactionTest()
        {
            var cityName = "New York";

            var itemsPage = new ItemsPage(DriverInstance);
            Assert.IsTrue(itemsPage.IsOppened());

            var selectedItemName = itemsPage.ClickAddLocationButton()
                                .SetValueToSearch(cityName).ClickSearchButton()
                                .SelectFirstLocationNameItem();
            Assert.IsTrue(itemsPage.IsOppened());

            var resultList = itemsPage.WaitForValueAddedInList(selectedItemName, 5).GetLocationsList();
            Assert.IsTrue(resultList.Contains(selectedItemName));

            itemsPage.SelectLocationInList(selectedItemName).ClickDeleteLocationButton();
            resultList = itemsPage.GetLocationsList();
            Assert.IsFalse(resultList.Contains(selectedItemName));
        }
    }
}
