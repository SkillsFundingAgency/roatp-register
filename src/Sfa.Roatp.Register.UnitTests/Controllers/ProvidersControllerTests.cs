using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Esfa.Roatp.ApplicationServices.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Roatp.Register.Web.Controllers;
using SFA.DAS.NLog.Logger;
using SFA.Roatp.Api.Types;

namespace Sfa.Roatp.Register.UnitTests.Controllers
{
    [TestFixture]
    public class ProvidersControllerTests
    {
        [TestCase(123456789)]
        [TestCase(12)]

        public void ShouldThrowBadRequestIfUkprnIsInvalid(int ukprn)
        {
            // Arrange
            var sut = new ProvidersController(null, null);

            // Act
            TestDelegate testDelegate = () => sut.Get(ukprn);

            // Assert
            var exception = Assert.Throws<HttpResponseException>(testDelegate);
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Response.StatusCode);
        }

        //        private ProvidersController _sut;
        //        private Mock<IGetProviders> _mockGetProviders;
        //        private Mock<ILog> _mockLogger;

        //        [SetUp]
        //        public void Init()
        //        {
        //            _mockGetProviders = new Mock<IGetProviders>();
        //            _mockLogger = new Mock<ILog>();

        //            _sut = new ProvidersController(
        //                _mockGetProviders.Object,
        //                _mockLogger.Object);
        //        }

        //        [Test]
        //        public void ShouldReturnProvider()
        //        {
        //            var expected = new ProviderDocument { Ukprn = 1 };

        //            _sut.Request = new HttpRequestMessage
        //            {
        //                RequestUri = new Uri("http://localhost/providers")
        //            };
        //            _sut.Configuration = new HttpConfiguration();
        //            _sut.Configuration.Routes.MapHttpRoute(
        //                name: "DefaultApi",
        //                routeTemplate: "{controller}/{id}",
        //                defaults: new { id = RouteParameter.Optional });

        //            _sut.RequestContext.RouteData = new HttpRouteData(
        //                route: new HttpRoute(),
        //                values: new HttpRouteValueDictionary { { "controller", "providers" } });

        //            //_mockGetProviders.Setup(x => x.GetProvider(1)).Returns(expected);

        //            var actual = _sut.Get(1);

        //            actual.Ukprn.ShouldBeEquivalentTo(expected.Ukprn);
        //            actual.Uri.Should().Be("http://localhost/Providers/1");
        //        }
    }
}
