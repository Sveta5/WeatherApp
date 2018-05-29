using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using UITests.Driver;

namespace UITests
{
    [TestFixture]
    public abstract class BaseWeatherAppTest
    {
        private const string WearherAppAutomationId = "0d599d98-d9a7-4453-9f7d-b76ccfa9e3b9_2jv79xvh9a28c!App";//";
        public const string WinAppDriverUri = "http://127.0.0.1:4723";
        private WinAppDriver WinAppDriver = new WinAppDriver();
        protected WindowsDriver<WindowsElement> DriverInstance;

        [OneTimeSetUp]
        public void StartDriver()
        {
            WinAppDriver.StartDriver();
        }

        [SetUp]
        public void SetupSession()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", WearherAppAutomationId);
            appCapabilities.SetCapability("platformName", "Windows");

            DriverInstance = WinAppDriver.GetSessionWithRetry(appCapabilities, 10);

            DriverInstance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void QuitSession()
        {
            DriverInstance.Quit();
        }

        [OneTimeTearDown]
        public void CloseDriver()
        {
            WinAppDriver.CloseDriver();
        }
    }
}
