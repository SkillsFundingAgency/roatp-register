using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{
    public abstract class RoatpWebDriver : IRoatpWebDriver
    {
        public RemoteWebDriver webDriver { get; set; }

        public RoatpWebDriver(RemoteWebDriver remoteWebDriver, int timeout)
        {
            remoteWebDriver.ConfigureTimeOut(timeout);
            webDriver = remoteWebDriver;
        }

        public string BrowserName
        {
            get { return webDriver.Capabilities.BrowserName; }
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
