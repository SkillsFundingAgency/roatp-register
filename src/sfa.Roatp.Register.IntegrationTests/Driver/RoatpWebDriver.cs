using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{
    public abstract class RoatpWebDriver : IRoatpWebDriver
    {
        private readonly int _defaultTimeOutinSec;

        public RemoteWebDriver webDriver { get; set; }

        public RoatpWebDriver(RemoteWebDriver remoteWebDriver, int timeout)
        {
            _defaultTimeOutinSec = timeout;
            webDriver = remoteWebDriver;
            ManageWebDriver();
        }

        private void ManageWebDriver()
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_defaultTimeOutinSec);
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_defaultTimeOutinSec);
            webDriver.Manage().Window.Maximize();
        }

        public string BrowserName
        {
            get { return webDriver.Capabilities.BrowserName; }
        }

        public void GoToURL(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }

        public void TakeScreenshot(string fileName)
        {
            try
            {
                var shot = webDriver.TakeScreenshot();
                shot.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
            }
            catch (Exception screenshotException)
            {
                Console.WriteLine(string.Format("'{0}' occured in TakeScreenshot", screenshotException.Message));
            }
        }
    }
}
