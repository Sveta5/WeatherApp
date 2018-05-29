using CodedUITests;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CodedUITestProject.TestCases
{
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class SmokeTests : BaseTest
    {   
        public SmokeTests()
        {
        }

        [TestMethod]
        public void AddAndDeleteLocationFunctionalityTest()
        {
            Task.Delay(1000);
            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.WaitForControlExist(10000);
            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAddLocationButton.WaitForControlExist(10000);
            var startItemsCount = UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.Items.Count;

            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAddLocationButton);
            
            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UIZipCodeEntryEdit.WaitForControlExist(3000);
            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UIZipCodeEntryEdit.WaitForControlEnabled(3000);
            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UISearchAgainButton.WaitForControlEnabled(3000);

            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UIZipCodeEntryEdit.Text = "2";
            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UISearchAgainButton);
            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UISearchedItemsListVieList.Items.First());

            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.WaitForControlExist(5000);
            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.WaitForControlEnabled(5000);
            
            var itemsAfterAdding = UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.Items;
            Assert.AreEqual(startItemsCount + 1, itemsAfterAdding.Count, "Location was not added");

            Gesture.Tap(itemsAfterAdding.First());
            
            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIDeleteSelectedButton);
            var itemsAfterDeliting = UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.Items;
            Assert.AreEqual(startItemsCount, itemsAfterDeliting.Count, "Location was not added");
        }

        [TestMethod]
        public void SearchLocationFunctionalityTest()
        {
            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAllLocationsListList.WaitForControlExist(10000);
            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAddLocationButton.WaitForControlExist(10000);

            var postalCode = "16802";
            UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAddLocationButton.WaitForControlExist(10000);
            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow.UIYourLocationsListTabList.UIXamarinFormsNavigatiTabPage.UIAddLocationButton);
            
            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UIZipCodeEntryEdit.WaitForControlExist(3000);
            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UIZipCodeEntryEdit.WaitForControlEnabled(3000);
            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UISearchAgainButton.WaitForControlEnabled(3000);

            UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UIZipCodeEntryEdit.Text = postalCode;
            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UIItemCustom1.UISearchAgainButton);
            var resultItems = UIMap.UIMyWeatherAppUWPWindow.UIItemCustom.UISearchedItemsListVieList.Items;

            Assert.AreNotEqual(0, resultItems.Count, "There is no search results");
        }
    }
}
