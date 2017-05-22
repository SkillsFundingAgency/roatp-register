using TechTalk.SpecFlow;
using BoDi;
using Esfa.Roatp.Register.UserAcceptanceTests.Driver;
using System.Reflection;
using System.IO;

namespace Esfa.Roatp.Register.UserAcceptanceTests.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private string _assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _url;
        public static string _RemoteDriverUri;
        public static int _defaultTimeoutinSec;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _url = Settings.AutUrl;
            _RemoteDriverUri = Settings.BrowserStackUri;
            _defaultTimeoutinSec = Settings.DefaultTimeoutinSec;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var roatpWebDriver = new PhantomJSRoatpWebDriver(_assemblyFolder, _defaultTimeoutinSec);
            _objectContainer.RegisterInstanceAs<IRoatpWebDriver>(roatpWebDriver);
            _objectContainer.RegisterInstanceAs(new RoatpUri { MainUrl = _url });
        }

        [AfterScenario]
        public void AferScenario()
        {

        }
    }
}
