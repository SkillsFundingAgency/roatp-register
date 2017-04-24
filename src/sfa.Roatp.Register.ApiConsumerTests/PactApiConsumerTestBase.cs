using NUnit.Framework;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace Sfa.Roatp.Register.PactTests
{
    public abstract class PactApiConsumerTestBase
    {
        public int MockServerPort => 88;

        public string MockServiceProviderBaseUri => $"http://localhost:{MockServerPort}";

        public IPactBuilder PactBuilder { get; set; }

        public IMockProviderService MockServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void OneTimeTestSetUp()
        {
            PactBuilder = new PactBuilder(new PactConfig { LogDir = "../logs", PactDir = "../pacts"})
                              .ServiceConsumer("Roatp API .Net Client")
                              .HasPactWith("Roatp API");

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