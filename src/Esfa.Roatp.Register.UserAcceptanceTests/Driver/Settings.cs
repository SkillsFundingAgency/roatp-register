using System.Configuration;

namespace Esfa.Roatp.Register.UserAcceptanceTests.Driver
{
    /// <summary>
    /// Resolves application setting configuration items
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the Application Under Test URL from App.config.
        /// </summary>
        public static string AutUrl => (ConfigurationManager.AppSettings["AUT.URL"]).TrimEnd('/');

        public static string BrowserStackUri => ConfigurationManager.AppSettings["BrowserStack.URI"];

        public static int DefaultTimeoutinSec => int.Parse(ConfigurationManager.AppSettings["AUT.URL.DefaultTimeoutinSec"]);
    }
}
