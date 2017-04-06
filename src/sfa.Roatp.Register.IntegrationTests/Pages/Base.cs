using OpenQA.Selenium.Support.PageObjects;
using sfa.Roatp.Register.IntegrationTests.Driver;

namespace sfa.Roatp.Register.IntegrationTests.Pages
{
    public abstract class Base
    {
        public IRoatpWebDriver Driver;
        public Base(IRoatpWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver.webDriver, this);
        }
    }
}
