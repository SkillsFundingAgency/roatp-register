using System.ComponentModel;
using System.Configuration;

namespace sfa.Roatp.Register.IntegrationTests.Driver
{
    /// <summary>
    /// Resolves application setting configuration items
    /// </summary>
    public static class ResolveAppConfig
    {
        /// <summary>
        /// Gets the Application Under Test URL from App.config.
        /// </summary>
        public static string GetSiteUrl()
        {
            return ConfigurationManager.AppSettings["AUT.URL"];
        }

        public static string GetBrowserStackUri()
        {
            return ConfigurationManager.AppSettings["BrowserStack.URI"];
        }

        public static int GetDefaultTimeoutinSec()
        {
            return (int)(TypeDescriptor.GetConverter(typeof(int))).ConvertFromString(ConfigurationManager.AppSettings["AUT.URL.DefaultTimeoutinSec"]);
        }
    }
}
