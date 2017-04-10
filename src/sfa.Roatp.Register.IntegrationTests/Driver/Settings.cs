using System.Configuration;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{
    /// <summary>
    /// Resolves application setting configuration items
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the Application Under Test URL from App.config.
        /// </summary>
        public static string AutUrl => ConfigurationManager.AppSettings["AUT.URL"];

        public static string BrowserStackUri => ConfigurationManager.AppSettings["BrowserStack.URI"];

        public static int DefaultTimeoutinSec => int.Parse(ConfigurationManager.AppSettings["AUT.URL.DefaultTimeoutinSec"]);
    }
}
