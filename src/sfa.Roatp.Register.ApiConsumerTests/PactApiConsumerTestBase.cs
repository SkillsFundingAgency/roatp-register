using NUnit.Framework;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace sfa.Roatp.Register.ApiConsumerTests
{
    public abstract class PactApiConsumerTestBase
    {
        private string _providerName => PactAttribute.GetName<PactProviderAttribute>(this).ProviderName;
        private string _consumerName => PactAttribute.GetName<PactConsumerAttribute>(this).ConsumerName;

        public int MockServerPort => 8080;

        public string MockServiceProviderBaseUri => $"http://localhost:{MockServerPort}";

        public IPactBuilder PactBuilder { get; set; }

        public IMockProviderService MockServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void OneTimeTestSetUp()
        {

            System.Console.WriteLine($"Pact between Provider {_providerName} and Consumer {_consumerName}");

            PactBuilder = new PactBuilder(new PactConfig { LogDir = "../logs", PactDir = "../pacts" })
                              .ServiceConsumer(_consumerName)
                              .HasPactWith(_providerName);
                              

            MockServiceProvider = PactBuilder.MockService(MockServerPort);

            MockServiceProvider.ClearInteractions();
        }

        [OneTimeTearDown]
        public void OneTimeTestTearDown()
        {
            PactBuilder.Build();
        }

    }
}