using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UITests.PageObject
{
    public class ItemsPage : BasePage
    {
        private WindowsElement RootElement => DriverSession.FindElementByAccessibilityId("RootElement");
        private WindowsElement AddLocationButton => DriverSession.FindElementByAccessibilityId("AddButton");
        private WindowsElement DeleteLocationButton => DriverSession.FindElementByAccessibilityId("DeleteButton");
        private IReadOnlyCollection<WindowsElement> LocationsItems => DriverSession.FindElementsByAccessibilityId("LocationsItem");


        public ItemsPage(WindowsDriver<WindowsElement> driverSession) : base(driverSession)
        {
        }

        public bool IsOppened()
        {
            return RootElement.Displayed;
        }

        public SearchPage ClickAddLocationButton()
        {
            AddLocationButton.Click();
            return new SearchPage(DriverSession);
        }

        public ItemsPage ClickDeleteLocationButton()
        {
            DeleteLocationButton.Click();
            return this;
        }

        public IEnumerable<string> GetLocationsList()
        {
            return LocationsItems.Select(item => item.Text);
        }

        public ItemsPage WaitForValueAddedInList(string value, int attemptsCount)
        {
            for (; attemptsCount > 0; attemptsCount--)
            {
                if (LocationsItems.Any(item => item.Text.Equals(value))) break;
                Task.Delay(500);
            }
            return this;
        }

        public ItemsPage SelectLocationInList(string selectedItemName)
        {
            var firstItem = LocationsItems.First(item => item.Text.Equals(selectedItemName));

            DriverSession.Mouse.Click(firstItem.Coordinates);
            return this;
        }
    }
}
