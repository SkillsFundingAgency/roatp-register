using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{

    public static class WebDriverExtension
    {
        public static IWebDriver ConfigureTimeOut(this IWebDriver driver, int timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout);
            driver.Manage().Window.Maximize();
            return driver;
        }
        public static void GoToUrl(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}
