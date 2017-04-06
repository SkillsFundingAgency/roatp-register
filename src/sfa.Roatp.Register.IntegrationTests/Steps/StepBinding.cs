using BoDi;
using sfa.Roatp.Register.IntegrationTests.Driver;
using sfa.Roatp.Register.IntegrationTests.Pages;
using System.Net;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace sfa.Roatp.Register.IntegrationTests.Steps
{
    [Binding]
    public class StepBinding
    {
        private readonly IObjectContainer _objectContainer;
        public StepBinding(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;

        }
        [Given(@"I can open roatp website")]
        public void GivenICanOpenRoatpWebsite()
        {
            var roatpwebdriver = _objectContainer.Resolve<IRoatpWebDriver>();
            var roatpUri = _objectContainer.Resolve<RoatpUri>();
            roatpwebdriver.GoToURL(roatpUri.MainUrl);
        }

        [When(@"I request for SFA Roatp csv file")]
        public void WhenIRequestForSFARoatpCsvFile()
        {
            var roatpwebdriver = _objectContainer.Resolve<IRoatpWebDriver>();
            var roatpUri = _objectContainer.Resolve<RoatpUri>();
            RoatpRegisterPage roatpregisterPage = new RoatpRegisterPage(roatpwebdriver);
            var responce = roatpregisterPage.ClickCSVLink(roatpUri.MainUrl);
            _objectContainer.RegisterInstanceAs(responce);

        }

        [Then(@"I should have a csv file with contents\.")]
        public void ThenIShouldHaveACsvFileWithContents_()
        {
            var responce = _objectContainer.Resolve<HttpWebResponse>();
            StringAssert.AreEqualIgnoringCase("text/csv", responce.ContentType, "Content Type is not text/csv");
            Assert.GreaterOrEqual(responce.ContentLength, 5000);
        }
    }
}
