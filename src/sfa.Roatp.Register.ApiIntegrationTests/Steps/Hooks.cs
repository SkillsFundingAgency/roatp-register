using System;
using BoDi;
using sfa.Roatp.Register.ApiIntegrationTests.Infrastructure;
using Sfa.Roatp.Register.Web.Controllers;
using TechTalk.SpecFlow;
using Moq;
using SFA.DAS.NLog.Logger;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace sfa.Roatp.Register.ApiIntegrationTests.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private Mock<ILog> _mockLogger;
        private ProvidersController _sut;
        private const string _uri = "http://localhost/providers";

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _mockLogger = new Mock<ILog>();
        }
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            var stubRepo = new StubProviderRepository();

            _sut = new ProvidersController(stubRepo, _mockLogger.Object);

            _sut.Request = new HttpRequestMessage
            {
                RequestUri = new Uri(_uri)
            };
            
            _sut.Configuration = new HttpConfiguration();

            _sut.Configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });

            _sut.RequestContext.RouteData = new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary { { "controller", "providers" } });

            _objectContainer.RegisterInstanceAs(_sut, "sut");

            _objectContainer.RegisterInstanceAs(stubRepo, "StubRepo");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _sut.Dispose();
        }
    }
}
