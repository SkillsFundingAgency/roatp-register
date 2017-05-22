using TechTalk.SpecFlow;
using BoDi;
using sfa.Roatp.Register.IntegrationTests.Steps;
using System.Configuration;
using Esfa.Roatp.Register.DeploymentTests.Pages;

namespace Esfa.Roatp.Register.DeploymentTests.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private static string _url;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _url = (ConfigurationManager.AppSettings["AUT.URL"]).TrimEnd('/');
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var roatpUri = new RoatpUri { MainUrl = _url };
            RoatpRegisterPage roatpregisterPage = new RoatpRegisterPage(roatpUri.MainUrl);
            _objectContainer.RegisterInstanceAs(roatpregisterPage);
        }
    }
}
