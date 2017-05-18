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
using Esfa.Roatp.ApplicationServices.Services;
using Sfa.Roatp.Register.Web.DependencyResolution;

namespace sfa.Roatp.Register.ApiIntegrationTests.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private ProvidersController _sut;
        private const string _uri = "http://localhost/providers";

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            var container = IoC.Initialize();
            var mockContext = new Mock<IRequestContext>();
            container.Configure(x =>
            {
                x.For<IGetProviders>().Use<StubProviderRepository>().Singleton();
                x.For<IRequestContext>().Use(y => mockContext.Object);
                x.For<ILog>().Use(y => new Mock<ILog>().Object);
            });

            _sut = container.GetInstance<ProvidersController>();

            _sut.Request = new HttpRequestMessage
            {
                RequestUri = new Uri(_uri)
            };

            _sut.Configuration = new HttpConfiguration();

            _sut.Configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });

            _sut.RequestContext.RouteData = new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary { { "controller", "providers" } });

            _objectContainer.RegisterInstanceAs(_sut, "sut");

            _objectContainer.RegisterInstanceAs(container.GetInstance<IGetProviders>(), "StubRepo");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _sut.Dispose();
        }
    }
}
