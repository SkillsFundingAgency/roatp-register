using BoDi;
using sfa.Roatp.Register.IntegrationTests.Driver;
using sfa.Roatp.Register.IntegrationTests.Pages;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System;
using System.IO;

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
            roatpwebdriver.webDriver.GoToUrl(roatpUri.MainUrl);
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

        [Then(@"I should have a csv file with more than (.*) Kb contents")]
        public void ThenIShouldHaveACsvFileWithMoreThanKbContents(int contentlength)
        {
            var responce = _objectContainer.Resolve<HttpWebResponse>();
            StringAssert.AreEqualIgnoringCase("text/csv", responce.ContentType, "Content Type is not text/csv");
            Assert.Greater(responce.ContentLength, contentlength);
        }
        
        [Then(@"All links should be accessible")]
        public void ThenAllLinksShouldBeAccessible()
        {
            var roatpwebdriver = _objectContainer.Resolve<IRoatpWebDriver>();
            RoatpRegisterPage roatpregisterPage = new RoatpRegisterPage(roatpwebdriver);
            var brokenLinks = roatpregisterPage.ArePageLinksWorking();

            Assert.AreEqual(0, brokenLinks.Count, $"{string.Join(Environment.NewLine, brokenLinks)} links are broken");
        }
        
        [Then(@"csv file should contain following information")]
        public void ThenCsvFileShouldContainFollowingInformation(Table table)
        {
            var responce = _objectContainer.Resolve<HttpWebResponse>();
            var webresponce = new StreamReader(responce.GetResponseStream()).ReadToEnd();
            List<string> content = webresponce.Replace(Environment.NewLine, ",").Split(',').ToList();
            List<string> tableList = table.Rows.Select(x => x["UKPRN"] as string).ToList();
            var NotindownloadedCsv = tableList.Except(content).ToList();
            Assert.IsTrue(!NotindownloadedCsv.Any(), $"{string.Join(Environment.NewLine, NotindownloadedCsv)} is/are not found in the downloadable csv");
        }
    }
}
