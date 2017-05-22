using BoDi;
using Esfa.Roatp.Register.UserAcceptanceTests.Driver;
using Esfa.Roatp.Register.UserAcceptanceTests.Pages;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System;
using System.IO;
using CsvHelper;

namespace Esfa.Roatp.Register.UserAcceptanceTests.Steps
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
            _objectContainer.RegisterInstanceAs(responce, "csv");
        }

        [Then(@"I should have a csv file with more than (.*) Kb contents")]
        public void ThenIShouldHaveACsvFileWithMoreThanKbContents(int contentlength)
        {
            var responce = _objectContainer.Resolve<HttpWebResponse>("csv");
            StringAssert.AreEqualIgnoringCase("text/csv", responce.ContentType, "Content Type is not text/csv");
            Assert.Greater(responce.ContentLength, contentlength);
        }

        [Then(@"I should have a csv file with below columns")]
        public void ThenIShouldHaveACsvFileWithBelowColumns(Table table)
        {
            var NotindownloadedCsv = CompareColumnsWithCsv(table);
            Assert.IsTrue(!NotindownloadedCsv.Any(), $"{string.Join(Environment.NewLine, NotindownloadedCsv)} is/are not found in the downloadable csv");
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
            var NotindownloadedCsv = CompareUkprnwithCsv(table);
            Assert.IsTrue(!NotindownloadedCsv.Any(), $"{string.Join(Environment.NewLine, NotindownloadedCsv)} is/are not found in the downloadable csv");
        }

        [Then(@"csv file should not contain following information")]
        public void ThenCsvFileShouldNotContainFollowingInformation(Table table)
        {
            
            var NotindownloadedCsv = CompareUkprnwithCsv(table);
            List<string> tableList = table.Rows.Select(x => x["UKPRN"] as string).ToList();
            Assert.IsTrue((NotindownloadedCsv.All(tableList.Contains) && NotindownloadedCsv.Count == tableList.Count),
                $"{ string.Join(Environment.NewLine, tableList.Except(NotindownloadedCsv).ToList())} is/are found in the downloadable csv");
        }

        [Then(@"I should have total (.*) Providers")]
        public void ThenIShouldHaveTotalProviders(int totalprovider)
        {
            List<string[]> roatpProviders = GetDtoFromCsv();
            int noOfRoatpProviders = roatpProviders.Count - 1; // Remove 1 for the header;
            Assert.AreEqual(totalprovider, noOfRoatpProviders, $"We expect {totalprovider} in the downloadable csv but it is {noOfRoatpProviders}");
        }

        [Then(@"I should have atleast (.*) Providers")]
        public void ThenIShouldHaveAtleastProviders(int totalprovider)
        {
            List<string[]> roatpProviders = GetDtoFromCsv();
            int noOfRoatpProviders = roatpProviders.Count - 1; // Remove 1 for the header;
            Assert.GreaterOrEqual(noOfRoatpProviders, totalprovider, $"We expect atleast {totalprovider} in the downloadable csv but it is {noOfRoatpProviders}");
        }

        private List<string> CompareUkprnwithCsv(Table table)
        {
            List<string[]> roatpProviders = GetDtoFromCsv();
            List<string> content = roatpProviders.Select(x => x[0] as string).ToList();
            List<string> tableList = table.Rows.Select(x => x["UKPRN"] as string).ToList();
            return tableList.Except(content).ToList();
        }

        private List<string> CompareColumnsWithCsv(Table table)
        {
            List<string> csvHeaders = GetDtoFromCsv().First().ToList();
            List<string> tableHeaders = table.Rows.Select(x=> x["Columns"] as string).ToList();
            return tableHeaders.Except(csvHeaders).ToList();
        }

        private List<string[]> GetDtoFromCsv()
        {
            var responce = _objectContainer.Resolve<HttpWebResponse>("csv");

            using (var webresponce = new StreamReader(responce.GetResponseStream()))
            {
                using (var parser = new CsvParser(webresponce))
                {
                    List<string[]> listOfrow = new List<string[]>();
                    while (true)
                    {
                        var row = parser.Read();
                        if (row == null)
                        {
                            return listOfrow;
                        }
                        listOfrow.Add(row);
                    }
                }
            }
        }
    }
}
