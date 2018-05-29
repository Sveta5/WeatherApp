using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests.PageObject
{
    public abstract class BasePage
    {
        protected WindowsDriver<WindowsElement> DriverSession;

        protected BasePage(WindowsDriver<WindowsElement> driverSession)
        {
            DriverSession = driverSession;
        }
    }
}
