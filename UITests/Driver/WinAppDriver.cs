﻿using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;

namespace UITests.Driver
{
    public class WinAppDriver
    {
        public const string WinAppDriverUri = "http://127.0.0.1:4723";
        private Process process;

        public WinAppDriver()
        {
        }

        public void StartDriver()
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + "\"C:\\Program Files (x86)\\Windows Application Driver\\WinAppDriver.exe\"");
            process = Process.Start(processInfo);
        }

        public WindowsDriver<T> GetSessionWithRetry<T>(DesiredCapabilities appCapabilities, int retryCount) where T : IWebElement
        {
            WindowsDriver<T> DriverInstance = null;
            for (; retryCount > 0; retryCount--)
            {
                try
                {
                    DriverInstance = new WindowsDriver<T>(new Uri(WinAppDriverUri), appCapabilities, TimeSpan.FromMinutes(1));
                    break;
                }
                catch (Exception)
                {
                    Debug.WriteLine("Driver is not connected to application. Try again.");
                    continue;
                }
            }

            return DriverInstance;
        }

        public void CloseDriver()
        {
            process.CloseMainWindow();
        }
    }
}
