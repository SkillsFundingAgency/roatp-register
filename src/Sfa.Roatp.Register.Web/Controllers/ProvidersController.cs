using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Esfa.Roatp.ApplicationServices.Services;
using SourceProvider = Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderDocument;
using SourceType = Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderType;
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
        [SwaggerOperation("Head")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("providers/{ukprn}")]
        [ExceptionHandling]
        public void Head(int ukprn)
        {
            if (_providerRepo.GetProvider(ukprn) != null)
            {
                return;
            }

            throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                $"No provider with Ukprn {ukprn} found");
        }

        [SwaggerOperation("Get")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(Provider))]
        [Route("providers/{ukprn}")]
        [ExceptionHandling]
        public Provider Get(int ukprn)
        {
            var response = _providerRepo.GetProvider(ukprn);

            if (response == null || !response.IsDateValid(DateTime.UtcNow))
            {
                throw HttpResponseFactory.RaiseException(HttpStatusCode.NotFound,
                    $"No provider with Ukprn {ukprn} found");
            }

            var provider = ApiProviderMapper.Map(response);


            provider.Uri = Resolve(response.Ukprn);

            return provider;
        }

        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Provider>))]
        [Route("providers")]
        [ExceptionHandling]
        public IEnumerable<Provider> Get()
        {
            try
            {
                var response = _providerRepo.GetAllProviders().Where(x => x.IsDateValid(DateTime.UtcNow)).Select(ApiProviderMapper.Map).ToList();

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

        [SwaggerOperation("GetAllOk")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("providers")]
        [ExceptionHandling]
        public void Head()
        {
            _providerRepo.GetAllProviders().Where(x => x.IsDateValid(DateTime.UtcNow)).Select(ApiProviderMapper.Map);
        }

        private string Resolve(long ukprn)
        {
            return Url.Link("DefaultApi", new { controller = "Providers", id = ukprn });
        }
    }
}
