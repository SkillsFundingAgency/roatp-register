using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{
    public interface IRoatpWebDriver
    {
        //
        // Summary:
        //      Get or Set the RemoteWebdriver instance.
        RemoteWebDriver webDriver { get; set; }
        //
        // Summary:
        //      Get runtime browser instance name.
        string BrowserName { get; }
        //
        // Summary:
        //     Gets a OpenQA.Selenium.Screenshot object representing the image of the page on
        //     the screen.
        //
        // Parameters:
        //     fileName:
        //      Screenshot FileName. It is best to use full path 
        void TakeScreenshot(string fileName);
    }
}
