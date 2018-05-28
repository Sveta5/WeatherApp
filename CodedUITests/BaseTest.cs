using CodedUITests;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodedUITestProject.TestCases
{
    public class BaseTest
    {
        public BaseTest()
        {
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            Keyboard.SendKeys("", ModifierKeys.Windows);

            Gesture.Tap(UIMap.UIStartWindow.UIPinnedtilesList.UIMyWeatherAppUWPsgrouGroup.UIMyWeatherAppUWPListItem);
            UIMap.UIMyWeatherAppUWPWindow.WaitForControlExist(3000);
            UIMap.UIMyWeatherAppUWPWindow.WaitForControlReady(3000);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            Gesture.Tap(UIMap.UIMyWeatherAppUWPWindow1.UICloseMyWeatherAppUWPButton);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }

}
