using OpenQA.Selenium.Support.PageObjects;
using sfa.Roatp.Register.IntegrationTests.Driver;

namespace sfa.Roatp.Register.IntegrationTests.Pages
{
    public abstract class PageBase
    {
        public IRoatpWebDriver Driver;
        public PageBase(IRoatpWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver.webDriver, this);
        }
    }
}
