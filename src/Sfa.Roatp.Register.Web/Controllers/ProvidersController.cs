using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Esfa.Roatp.ApplicationServices.Services;
using Sfa.Roatp.Register.Web.Attributes;
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
        /// Check if provider exists
        /// </summary>
        /// <param name="ukprn">UKPRN</param>
        [SwaggerOperation("Head")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid UKPRN (should be 8 numbers long)")]
        [Route("providers/{ukprn}")]
        [ExceptionHandling]
        public void Head(int ukprn)
        {
            Get(ukprn);
        }

        /// <summary>
        /// Get a provider
        /// </summary>
        /// <param name="ukprn"></param>
        /// <returns></returns>
        [SwaggerOperation("Get")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(Provider))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid UKPRN (should be 8 numbers long)")]
        [Route("providers/{ukprn}")]
        [ExceptionHandling]
        public Provider Get(int ukprn)
        {
            if (ukprn.ToString().Length != 8)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var response = _providerRepo.GetProvider(ukprn);

            if (response == null || !response.IsDateValid(DateTime.UtcNow))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var provider = ApiProviderMapper.Map(response);


            provider.Uri = Resolve(response.Ukprn);

            return provider;
        }

        /// <summary>
        /// Get active providers
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Check if you can get active providers
        /// </summary>
        [SwaggerOperation("GetAllOk")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("providers")]
        [ExceptionHandling]
        public void Head()
        {
            Get();
        }

        private string Resolve(long ukprn)
        {
            return Url.Link("DefaultApi", new { controller = "Providers", id = ukprn });
        }
    }
}
