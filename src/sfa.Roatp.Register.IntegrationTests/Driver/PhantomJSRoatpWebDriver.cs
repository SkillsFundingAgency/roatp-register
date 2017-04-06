using OpenQA.Selenium.PhantomJS;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{
    public sealed class PhantomJSRoatpWebDriver : RoatpWebDriver
    {
        private static int _defaultTimeOutinSec = 10;

        public PhantomJSRoatpWebDriver(string filepath)
            : this(filepath, _defaultTimeOutinSec)
        {
        }

        public PhantomJSRoatpWebDriver(string filepath, int pageNavigationTimeout)
            : base(new PhantomJSDriver(filepath, Options()), pageNavigationTimeout)
        {
            _defaultTimeOutinSec = pageNavigationTimeout;
        }
        private static PhantomJSOptions Options()
        {
            var options = new PhantomJSOptions();
            return options;
        }
    }
}
