using System;
using System.Net;
using Elasticsearch.Net;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Moq;
using Nest;
using NUnit.Framework;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Infrastructure.Elasticsearch;
using SFA.DAS.NLog.Logger;
using SFA.Roatp.Api.Types;

namespace Sfa.Roatp.Register.UnitTests.Infrastructure.ElasticSearch
{
    [TestFixture]
    public class ProviderRepositoryTests
    {
        private Mock<IElasticsearchCustomClient> _elasticClient;

        private Mock<ILog> _log;

        [SetUp]
        public void Setup()
        {
            _elasticClient = new Mock<IElasticsearchCustomClient>();
            _log = new Mock<ILog>();
            _log.Setup(x => x.Warn(It.IsAny<string>())).Verifiable();
        }

        [Test]
        public void GetAllProvidersThrowExceptionInvalidStatusCode()
        {
            var searchResponse = new Mock<ISearchResponse<ProviderDocument>>();
            var apiCall = new Mock<IApiCallDetails>();
            apiCall.SetupGet(x => x.HttpStatusCode).Returns((int)HttpStatusCode.Ambiguous);
            searchResponse.SetupGet(x => x.ApiCall).Returns(apiCall.Object);

            var countResponse = new Mock<ICountResponse>();
            countResponse.SetupGet(x => x.Count).Returns(1);

            _elasticClient.Setup(x => x.Search(It.IsAny<Func<SearchDescriptor<ProviderDocument>, ISearchRequest>>(), It.IsAny<string>())).Returns(searchResponse.Object);
            var repo = new ProviderRepository(
                _elasticClient.Object,
                Mock.Of<IConfigurationSettings>(),
                _log.Object);

            _elasticClient.Setup(x => x.Count(It.IsAny<Func<CountDescriptor<Provider>, ICountRequest>>(), It.IsAny<string>())).Returns(countResponse.Object);

            Assert.Throws<ApplicationException>(() => repo.GetAllProviders());
        }

        [Test]
        public void GetProviderShouldLogWhenInvalidStatusCode()
        {
            var searchResponse = new Mock<ISearchResponse<ProviderDocument>>();
            var apiCall = new Mock<IApiCallDetails>();
            apiCall.SetupGet(x => x.HttpStatusCode).Returns((int)HttpStatusCode.Ambiguous);
            searchResponse.SetupGet(x => x.ApiCall).Returns(apiCall.Object);

            var countResponse = new Mock<ICountResponse>();
            countResponse.SetupGet(x => x.Count).Returns(1);

            _elasticClient.Setup(x => x.Search(It.IsAny<Func<SearchDescriptor<ProviderDocument>, ISearchRequest>>(), It.IsAny<string>())).Returns(searchResponse.Object);
            var repo = new ProviderRepository(
                _elasticClient.Object,
                Mock.Of<IConfigurationSettings>(),
                _log.Object);

            _elasticClient.Setup(x => x.Count(It.IsAny<Func<CountDescriptor<Provider>, ICountRequest>>(), It.IsAny<string>())).Returns(countResponse.Object);

            Assert.Throws<ApplicationException>(() => repo.GetProvider(123));
        }
    }
}