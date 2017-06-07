using BoDi;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.IO;
using CsvHelper;
using SFA.Roatp.Api.Types;
using Esfa.Roatp.Register.DeploymentTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [Given(@"I request for SFA Roatp csv file")]
        public void GivenIRequestForSFARoatpCsvFile()
        {
            var roatpregisterPage = _objectContainer.Resolve<RoatpRegisterPage>();
            var responce = roatpregisterPage.ClickCSVLink();
            _objectContainer.RegisterInstanceAs(responce, "csv");
        }

        [Given(@"I request for SFA Roatp Get All providers Api end point")]
        public void GivenIRequestForSFARoatpGetAllProvidersApiEndPoint()
        {
            var roatpregisterPage = _objectContainer.Resolve<RoatpRegisterPage>();
            var allproviders = roatpregisterPage.GetAllProvidersFromApi();
            _objectContainer.RegisterInstanceAs(allproviders, "allproviders");
        }

        [Then(@"they should expose same details")]
        public void ThenTheyShouldExposeSameDetails()
        {
            List<string[]> roatpProviders = GetDtoFromCsv();
            int noOfRoatpProvidersinCsv = roatpProviders.Count - 1; // Remove 1 for the header;
            var allproviders = _objectContainer.Resolve<List<Provider>>("allproviders");
            int noOfRoatpProvidersinApi = allproviders.Count();
            Assert.AreEqual(noOfRoatpProvidersinApi, noOfRoatpProvidersinCsv, $"We have {noOfRoatpProvidersinCsv} in the downloadable csv, and {noOfRoatpProvidersinApi} in the api");
        }

        [Then(@"I should have atleast (.*) Providers")]
        public void ThenIShouldHaveAtleastProviders(int totalprovider)
        {
            List<string[]> roatpProviders = GetDtoFromCsv();
            int noOfRoatpProviders = roatpProviders.Count - 1; // Remove 1 for the header;
            Assert.IsTrue(noOfRoatpProviders >= totalprovider, $"We expect atleast {totalprovider} in the downloadable csv but it is {noOfRoatpProviders}");
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
