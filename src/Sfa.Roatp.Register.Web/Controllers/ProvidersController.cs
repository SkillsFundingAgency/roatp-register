using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Esfa.Roatp.ApplicationServices.Services;
using Sfa.Roatp.Register.Web.Attributes;
using SFA.DAS.NLog.Logger;
using SFA.Roatp.Api.Client;
using SFA.Roatp.Api.Types;
using Swashbuckle.Swagger.Annotations;

namespace Sfa.Roatp.Register.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api")]
    public class ProvidersController : ApiController
    {
       // private readonly IGetProviders _providerRepo;
        private readonly IRoatpServiceApiClient _apiClient;
        private readonly ILog _log;

        public ProvidersController( ILog log, IRoatpServiceApiClient apiClient) //IGetProviders providerRepo,
        {
            //_providerRepo = providerRepo;
            _log = log;
            _apiClient = apiClient;
        }

        /// <summary>
        /// Check if provider exists
        /// </summary>
        /// <param name="ukprn">UKPRN</param>
        [SwaggerOperation("Head")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid UKPRN (should be 8 numbers long)")]
        [System.Web.Http.Route("providers/{ukprn}")]
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
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid UKPRN (should be 8 numbers long)")]
        [System.Web.Http.Route("providers/{ukprn}")]
        [ExceptionHandling]
        public Provider Get(int ukprn)
        {
            if (ukprn.ToString().Length != 8)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //var response = _providerRepo.GetProvider(ukprn);

            //if (response == null || !response.IsDateValid(DateTime.UtcNow))
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}

            //var providerOrig = ApiProviderMapper.Map(response);


            //providerOrig.Uri = Resolve(response.Ukprn);

            //return provider;

            if (ukprn == 11111111)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var provider = _apiClient.GetRoatpSummaryUkprn(ukprn).Result;

            if (provider == null || !provider.IsDateValid(DateTime.UtcNow))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            provider.Uri = Resolve(provider.Ukprn);

            return provider;
        }

        /// <summary>
        /// Get active providers
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(IEnumerable<Provider>))]
        [System.Web.Http.Route("providers")]
        [ExceptionHandling]
        public IEnumerable<Provider> Get()
        {
            try
            {
                //var response = _providerRepo.GetAllProviders().Where(x => x.IsDateValid(DateTime.UtcNow)).Select(ApiProviderMapper.Map).ToList();


                IEnumerable<Provider> providers = _apiClient.GetRoatpSummary().Result.Where(x => x.IsDateValid(DateTime.UtcNow));
                foreach (var provider in providers)
                {
                    provider.Uri = Resolve(provider.Ukprn);
                }

                //return response;
                return providers;
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
        [System.Web.Http.Route("providers")]
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
