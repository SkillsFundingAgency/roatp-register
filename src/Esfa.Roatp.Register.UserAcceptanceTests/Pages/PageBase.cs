using OpenQA.Selenium.Support.PageObjects;
using Esfa.Roatp.Register.UserAcceptanceTests.Driver;

namespace Esfa.Roatp.Register.UserAcceptanceTests.Pages
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
