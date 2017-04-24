using NUnit.Framework;
using SFA.Roatp.Api.Client;
using SFA.Roatp.Api.Types;
using System.Collections.Generic;
using System.Linq;
using PactNet.Mocks.MockHttpService.Models;

namespace Sfa.Roatp.Register.PactTests
{
    [TestFixture]
    public class RoatpAPIConsumerTests : PactApiConsumerTestBase
    {
        List<RoatpProvider> expected = new List<RoatpProvider> {
            new RoatpProvider() { Ukprn = 10002145, ProviderType = ProviderType.MainProvider, Name = "Coventry Provider" },
            new RoatpProvider() { Ukprn = 10002146, ProviderType = ProviderType.EmployerProvider, Name = "Birmingham Provider"  },
            new RoatpProvider() { Ukprn = 10002147, ProviderType= ProviderType.SupportingProvider, Name = "Warwick Provider" }};

        [Test]
        public void ShouldGetARoatpProvidersByUkprn()
        {
            const string providerukprn = "10002145";

            MockServiceProvider
                .Given("There is a Roatp providers having Ukprn as 10002145")
                .UponReceiving("A GET request to retrive a roatp providers")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/api/providers/{providerukprn}",
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type","application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        Ukprn = 10002145,
                        ProviderType = ProviderType.MainProvider,
                        Name = "Coventry Provider"
                    }
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Act
            var result = consumer.Get(providerukprn);

            //Assert
            Assert.IsTrue(result.Ukprn.ToString() == providerukprn, "Roatp provider UKPRN did not match");

            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldReturnTrueIfAProviderExists()
        {
            const string providerukprn = "10005555";

            MockServiceProvider
                 .Given("There is a Roatp providers exists with Ukprn as 10005555")
                .UponReceiving("A HEAD request to look for a roatp provider")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Head,
                    Path = $"/api/providers/{providerukprn}",
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 204,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type","application/json; charset=utf-8" }
                    },
                    Body = null
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Act
            var result = consumer.Exists(providerukprn);

            //Assert
            Assert.IsTrue(result, "Roatp provider UKPRN is not found");

            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldReturnFalseIfAProviderDoesNotExists()
        {
            const string providerukprn = "10005656";

            MockServiceProvider
                 .Given("There is a Roatp providers exists with Ukprn as 10005656")
                .UponReceiving("A HEAD request to look for a roatp provider")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Head,
                    Path = $"/api/providers/{providerukprn}",
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 404,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type","application/json; charset=utf-8" }
                    },
                    Body = null
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Act
            var result = consumer.Exists(providerukprn);

            //Assert
            Assert.IsFalse(result, "Roatp provider UKPRN is found");

            MockServiceProvider.VerifyInteractions();
        }
    }
}

