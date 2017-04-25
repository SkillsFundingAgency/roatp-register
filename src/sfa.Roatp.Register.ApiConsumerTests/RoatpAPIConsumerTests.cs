using NUnit.Framework;
using SFA.Roatp.Api.Client;
using SFA.Roatp.Api.Types;
using System.Collections.Generic;
using System.Linq;
using PactNet.Mocks.MockHttpService.Models;
using System.Net.Http;
using SFA.Roatp.Api.Types.Exceptions;

namespace sfa.Roatp.Register.ApiConsumerTests
{
    [TestFixture]
    [PactProvider("Roatp API")]
    [PactConsumer("Roatp API .Net Client")]
    public class RoatpAPIConsumerTests : PactApiConsumerTestBase
    {
        [Test]
        public void ShouldGetAllRoatpProviders()
        {
            MockServiceProvider
                .Given("There are Roatp providers")
                .UponReceiving("A GET request to retrive all roatp providers")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/api/providers",
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
                    Body = new []
                    {
                        new
                        {
                            Ukprn = 10000020,
                            Name = "5 E LTD.",
                            ProviderType = ProviderType.MainProvider,
                            ContractedForNonLeviedEmployers = false,
                            ParentCompanyGuarantee = false,
                            NewOrganisationWithoutFinancialTrackRecord = false,
                            Uri = @"https://ci-roatp.apprenticeships.sfa.bis.gov.uk/api/providers/10000020\"
                        },
                         new
                        {
                            Ukprn = 10000028,
                            Name = "WOODSPEEN TRAINING LIMITED",
                            ProviderType = ProviderType.MainProvider,
                            ContractedForNonLeviedEmployers = false,
                            ParentCompanyGuarantee = false,
                            NewOrganisationWithoutFinancialTrackRecord = false,
                            Uri = @"https://ci-roatp.apprenticeships.sfa.bis.gov.uk/api/providers/10000028\"
                        },
                    }
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Act
            var result = consumer.FindAll();

            //Assert
            Assert.IsTrue(result.Count() == 2, "Endpoint '/api/providers' failed");

            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldGetARoatpProvidersByUkprn()
        {
            const string providerukprn = "10002145";

            MockServiceProvider
                .Given("There is a Roatp providers having Ukprn as 10002145")
                .UponReceiving("A GET request to retrive a roatp provider")
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
            Assert.IsTrue(result.Ukprn.ToString() == providerukprn, "Endpoint '/api/providers/{providerukprn}' failed");

            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldReturn400ForBadUkprn()
        {
            const string providerukprn = "ABCDEF";

            MockServiceProvider
                .Given("That Ukprn ABCDEF is bad ukprn")
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
                    Status = 400,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type","application/json; charset=utf-8" }
                    },
                    Body = "\"Message\":\"The request is invalid.\"" 
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);
            
            //Assert
            Assert.Throws<HttpRequestException>(() => consumer.Get(providerukprn));
            
            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldReturn404ForNotFoundUkprn()
        {
            const string providerukprn = "12345678";

            MockServiceProvider
                .Given("That Ukprn 12345678 is not found")
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
                    Status = 404,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type","text/plain; charset=utf-8" }
                    },
                    Body = "No provider with Ukprn 12345678 found"
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Assert
            Assert.Throws<EntityNotFoundException>(() => consumer.Get(providerukprn));

            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldReturn204IfAProviderExists()
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
                    },
                    Body = null
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Act
            var result = consumer.Exists(providerukprn);

            //Assert
            Assert.IsTrue(result, "HttpVerb.Head on Endpoint '/api/providers/{providerukprn}' failed");

            MockServiceProvider.VerifyInteractions();
        }

        [Test]
        public void ShouldReturn404IfAProviderDoesNotExists()
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
                        {"Content-Type","text/plain; charset=utf-8" }
                    },
                    Body = null
                });

            var consumer = new RoatpApiClient(MockServiceProviderBaseUri);

            //Act
            var result = consumer.Exists(providerukprn);

            //Assert
            Assert.IsFalse(result, "HttpVerb.Head on Endpoint '/api/providers/{providerukprn}' failed");

            MockServiceProvider.VerifyInteractions();
        }
    }
}


