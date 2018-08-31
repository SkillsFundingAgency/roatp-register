using BoDi;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System;
using System.Globalization;
using NUnit.Framework;
using Sfa.Roatp.Register.Web.Controllers;
using sfa.Roatp.Register.ApiIntegrationTests.Infrastructure;
using SFA.Roatp.Api.Types;
using System.Web.Http;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Esfa.Roatp.Register.ApiSystemTests.Infrastructure;

namespace sfa.Roatp.Register.ApiIntegrationTests.StepBindings
{
    [Binding]
    public class Stepbindings : TechTalk.SpecFlow.Steps
    {
        private readonly IObjectContainer _objectContainer;

        public Stepbindings(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        #region Given

        [Given(@"the following roatp providers are available")]
        public void GivenTheFollowingRoatpProvidersAreAvailable(Table table)
        {
            var stubrepo = _objectContainer.Resolve<StubProviderRepository>("StubRepo");
            stubrepo.roatpProviderDocuments.AddRange(ToRoatpProviders(table));
            var availableroatpdata = stubrepo.roatpProviderDocuments;
        }

        [Given(@"A Roatp provider with future start date")]
        public void GivenARoatpProviderWithFutureStartDate()
        {
            var table = GetTable(DateTime.Now.AddDays(5).ToShortDateString(),"");
            Given(@"the following roatp providers are available", table);
            When(string.Format("I request for provider with Ukprn {0}", table.Rows[0][0]));
        }

        [Given(@"A Roatp provider with past end date")]
        public void GivenARoatpProviderWithPastEndDate()
        {
            var table = GetTable("20-Mar-2017", DateTime.Now.AddDays(-5).ToShortDateString());
            Given(@"the following roatp providers are available", table);
            When(string.Format("I request for provider with Ukprn {0}", table.Rows[0][0]));
        }

        [Given(@"A Roatp provider with future end date")]
        public void GivenARoatpProviderWithFutureEndDate()
        {
            var table = GetTable("20-Mar-2017", DateTime.Now.AddDays(5).ToShortDateString());
            Given(@"the following roatp providers are available", table);
            When(string.Format("I request for provider with Ukprn {0}", table.Rows[0][0]));
        }

        #endregion

        #region When

        [When(@"I request for All providers")]
        public void WhenIRequestForAllProviders()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            IEnumerable<Provider> result = sut.Get();
            _objectContainer.RegisterInstanceAs(result, "result");
        }
        
        [When(@"I request for provider with Ukprn (.*)")]
        public void WhenIRequestForProviderWithUkprn(int ukprn)
        {
            ScenarioContext.Current.Set(ukprn, "ukprn");
        }

        #endregion

        #region Then

        [Then(@"I should get an exception when i request for a Provider with past end date")]
        public void ThenIShouldGetAnExceptionWhenIRequestForAProviderWithPastEndDate()
        {
            AssertThrowsHttpResponceException();
        }

        [Then(@"I should get an exception when i request for a Provider with future start date")]
        public void ThenIShouldGetAnExceptionWhenIRequestForAProviderWithFutureStartDate()
        {
            AssertThrowsHttpResponceException();
        }

        [Then(@"I get all provider information")]
        public void ThenIShouldGetAllProviders()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");
            Assert.IsTrue(result.Count() == 3);
        }

        [Then(@"I get a single providers information")]
        public void ThenIShouldGetAProvider()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);
            Assert.IsTrue(result.Ukprn.ToString() == ukprn.ToString());
        }

        [Then(@"I should not get any exception when i request for an existence of Provider which can be found")]
        public void ThenIShouldNotGetAnyExceptionWhenIRequestForAnExistenceOfProviderWhichCanBeFound()
        {
            AssertDoesNotThrowsHttpResponceException();
        }

        [Then(@"I should not get any exception when i request for a Provider with future end date")]
        public void ThenIShouldNotGetAnyExceptionWhenIRequestForAProviderWithFutureEndDate()
        {
            AssertDoesNotThrowsHttpResponceException();
        }

        [Then(@"I should get an exception when i request for a Provider which can not be found")]
        public void ThenIShouldGetAnExceptionWhenIRequestForAProviderWhichCanNotBeFound()
        {
            AssertThrowsHttpResponceException();
        }

        [Then(@"I should get an exception when i request for an existence of Provider which can not be found")]
        public void ThenIShouldGetAnExceptionWhenIRequestForAnExistenceOfProviderWhichCanNotBeFound()
        {
            AssertThrowsHttpResponceException();
        }
        [Then(@"returns All Providers UKPRN field")]
        public void ThenReturnsAllProvidersUKPRNField()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");

            var firstResult = result.FirstOrDefault();

            Assert.IsTrue(firstResult.HasProperty("Ukprn"));
        }

        [Then(@"returns All Providers Name field")]
        public void ThenReturnsAllProvidersNameField()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");

            var firstResult = result.FirstOrDefault();

            Assert.IsTrue(firstResult.HasProperty("Name"));
        }

        [Then(@"returns All Providers NewOrganisationWithoutFinancialTrackRecord field")]
        public void ThenReturnsAllProvidersNewOrganisationWithoutFinancialTrackRecordField()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");

            var firstResult = result.FirstOrDefault();

            Assert.IsTrue(firstResult.HasProperty("NewOrganisationWithoutFinancialTrackRecord"));
        }

        [Then(@"returns All Providers ParentCompanyGuarantee field")]
        public void ThenReturnsAllProvidersParentCompanyGuaranteeField()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");

            var firstResult = result.FirstOrDefault();

            Assert.IsTrue(firstResult.HasProperty("ParentCompanyGuarantee"));
        }

        [Then(@"returns All Providers ProviderType field")]
        public void ThenReturnsAllProvidersProviderTypeNameField()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");

            var firstResult = result.FirstOrDefault();

            Assert.IsTrue(firstResult.HasProperty("ProviderType"));
        }

        [Then(@"returns All Providers StartDate field")]
        public void ThenReturnsAllProvidersStartDateField()
        {
            var result = _objectContainer.Resolve<IEnumerable<Provider>>("result");

            var firstResult = result.FirstOrDefault();

            Assert.IsTrue(firstResult.HasProperty("StartDate"));
        }

        [Then(@"returns UKPRN field")]
        public void ThenReturnsUKPRNField()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);

            Assert.IsTrue(result.HasProperty("Ukprn"));
        }

        [Then(@"returns Name field")]
        public void ThenReturnsNameField()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);

            Assert.IsTrue(result.HasProperty("Name"));
        }

        [Then(@"returns NewOrganisationWithoutFinancialTrackRecord field")]
        public void ThenReturnsNewOrganisationWithoutFinancialTrackRecordField()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);

            Assert.IsTrue(result.HasProperty("NewOrganisationWithoutFinancialTrackRecord"));
        }

        [Then(@"returns ParentCompanyGuarantee field")]
        public void ThenReturnsParentCompanyGuaranteeField()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);

            Assert.IsTrue(result.HasProperty("ParentCompanyGuarantee"));
        }

        [Then(@"returns ProviderType field")]
        public void ThenReturnsProviderTypeNameField()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);

            Assert.IsTrue(result.HasProperty("ProviderType"));
        }

        [Then(@"returns StartDate field")]
        public void ThenReturnsStartDateField()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            var result = sut.Get(ukprn);

            Assert.IsTrue(result.HasProperty("Ukprn"));
        }


        #endregion

        #region privatemethods

        private Table GetTable(string startDate, string endDate)
        {

            string[] header = { "Ukprn", "Name", "ProviderType", "ContractedForNonLeviedEmployers", "NewOrganisationWithoutFinancialTrackRecord", "ParentCompanyGuarantee", "StartDate", "EndDate" };
            string[] row1 = { "69992101", "MNO Institute", "MainProvider", "False", "True", "True", startDate, endDate };
            var table = new Table(header);
            table.AddRow(row1);
            return table;
        }

        private void AssertDoesNotThrowsHttpResponceException()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            Assert.DoesNotThrow(() => sut.Head(ukprn));
        }

        private void AssertThrowsHttpResponceException()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            Assert.Throws<HttpResponseException>(() => sut.Head(ukprn));
        }


        private List<ProviderDocument> ToRoatpProviders(Table table)
        {
            List<dynamic> tableData = table.CreateDynamicSet().ToList();

            List<ProviderDocument> roatpProviderdocuments = new List<ProviderDocument>();

            tableData.ForEach(x => roatpProviderdocuments.Add(CasttoRoatpProvider(x)));

            return roatpProviderdocuments;
        }

        private ProviderDocument CasttoRoatpProvider(dynamic x)
        {

            Func<dynamic,DateTime?> convertString = d =>
             {
                 DateTime result;
                 if (DateTime.TryParseExact(Convert.ChangeType(d, typeof(string)), "dd-MMM-yyyy", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out result))
                 {
                     return result;
                 }
                 return (d as string == "") ? null : d;
             };

            return new ProviderDocument
            {
                Ukprn = x.Ukprn,
                Name = x.Name,
                ProviderType = Enum.Parse(typeof(Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderType), x.ProviderType, true),
                ContractedForNonLeviedEmployers = x.ContractedForNonLeviedEmployers,
                NewOrganisationWithoutFinancialTrackRecord = x.NewOrganisationWithoutFinancialTrackRecord,
                ParentCompanyGuarantee = x.ParentCompanyGuarantee,
                StartDate = convertString(x.StartDate),
                EndDate = convertString(x.EndDate)
            };
        }

        #endregion
    }
}