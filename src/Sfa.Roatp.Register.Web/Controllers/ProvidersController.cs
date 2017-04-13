using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Sfa.Roatp.Register.Core.Services;
using Sfa.Roatp.Register.Web.Attributes;
using Sfa.Roatp.Register.Web.Helpers;
using SFA.DAS.NLog.Logger;
using SFA.Roatp.Api.Types;
using Swashbuckle.Swagger.Annotations;

namespace Sfa.Roatp.Register.Web.Controllers
{
    [RoutePrefix("api")]
    public class ProvidersController : ApiController
    {
        private readonly IGetProviders _providerRepo;
        private readonly ILog _log;
        
        public ProvidersController(IGetProviders providerRepo, ILog log)
        {
            _providerRepo = providerRepo;
            _log = log;
        }

        /// <summary>
        /// Provider exists?
        /// </summary>
        /// <param name="ukprn">UKPRN</param>
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("providers/{ukprn}")]
        [ExceptionHandling]
        public void Head(long ukprn)
        {
            if (_providerRepo.GetProvider(ukprn) != null)
            {
                return;
            }

            throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                $"No provider with Ukprn {ukprn} found");
        }

        [SwaggerOperation("Get")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(RoatpProvider))]
        [Route("providers/{ukprn}")]
        [ExceptionHandling]
        public RoatpProvider Get(long ukprn)
        {
            var response = _providerRepo.GetProvider(ukprn);

            if (response == null)
            {
                throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                    $"No provider with Ukprn {ukprn} found");
            }

            response.Uri = Resolve(response.Ukprn);

            return response;
        }

        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<RoatpProvider>))]
        [Route("providers")]
        [ExceptionHandling]
        public IEnumerable<RoatpProvider> Get()
        {
            try
            {
                var response = _providerRepo.GetAllProviders();

                foreach (var provider in response)
                {
                    provider.Uri = Resolve(provider.Ukprn);
                }

                return response;
            }
            catch (Exception e)
            {
                _log.Error(e, "/providers");
                throw;
            }
        }
        
        private string Resolve(long ukprn)
        {
            return Url.Link("DefaultApi", new { controller = "Providers", id = ukprn });
        }
    }
}
