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

namespace sfa.Roatp.Register.ApiIntegrationTests.StepBindings
{
    [Binding]
    public class Stepbindings
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
            stubrepo.roatpProviders.AddRange(ToRoatpProviders(table));
            var availableroatpdata = stubrepo.roatpProviders;
        }

        #endregion

        #region When

        [When(@"I request for All providers")]
        public void WhenIRequestForAllProviders()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            IEnumerable<RoatpProvider> result = sut.Get();
            _objectContainer.RegisterInstanceAs(result, "result");
        }

        [When(@"I request for A provider with Ukprn as (.*)")]
        public void WhenIRequestForAProviderWithUkprnAs(int ukprn)
        {
            ScenarioContext.Current.Set(ukprn, "ukprn");
        }

        #endregion

        #region Then

        [Then(@"I should get All providers")]
        public void ThenIShouldGetAllProviders()
        {
            var result = _objectContainer.Resolve<IEnumerable<RoatpProvider>>("result");
            Assert.IsTrue(result.Count() == 3);
        }

        [Then(@"I should get A provider")]
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
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            Assert.DoesNotThrow(() => sut.Head(ukprn));
        }

        [Then(@"I should get an exception when i request for a Provider which can not be found")]
        public void ThenIShouldGetAnExceptionWhenIRequestForAProviderWhichCanNotBeFound()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            Assert.Throws<HttpResponseException>(() => sut.Get(ukprn));
        }

        [Then(@"I should get an exception when i request for an existence of Provider which can not be found")]
        public void ThenIShouldGetAnExceptionWhenIRequestForAnExistenceOfProviderWhichCanNotBeFound()
        {
            var sut = _objectContainer.Resolve<ProvidersController>("sut");
            var ukprn = ScenarioContext.Current.Get<int>("ukprn");
            Assert.Throws<HttpResponseException>(() => sut.Head(ukprn));
        }

        #endregion

        #region privatemethods

        private List<RoatpProvider> ToRoatpProviders(Table table)
        {
            List<dynamic> tableData = table.CreateDynamicSet().ToList();

            List<RoatpProvider> roatpProvider = new List<RoatpProvider>();

            tableData.ForEach(x => roatpProvider.Add(CasttoRoatpProvider(x)));

            return roatpProvider;
        }

        private RoatpProvider CasttoRoatpProvider(dynamic x)
        {

            Func<dynamic,DateTime?> convertString = d =>
             {
                 DateTime result;
                 if (DateTime.TryParseExact(Convert.ChangeType(d, typeof(string)), "dd/MMM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result))
                 {
                     return result;
                 }
                 return null;
             };

            return new RoatpProvider
            {
                Ukprn = x.Ukprn,
                Name = x.Name,
                ProviderType = Enum.Parse(typeof(ProviderType), x.ProviderType, true),
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