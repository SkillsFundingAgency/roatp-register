using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.Roatp.Api.Types;

namespace SFA.Roatp.Api.Client
{
    public class RoatpServiceApiClient : ApiClientBase, IRoatpServiceApiClient
    {
       // private readonly IConfigurationSettings _applicationSettings;

        public RoatpServiceApiClient(): base("http://localhost:37951") // MFCMFC base("https://roatp.apprenticeships.sfa.bis.gov.uk")
        {
           // _applicationSettings = applicationSettings;
            //_applicationSettings = applicationSettings;
            // private ConfigurationSettings _applicationSettings;
        }

        public Task<IEnumerable<IDictionary<string, object>>> GetRoatpSummary()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDictionary<string, object>>> GetRoatpSummaryUkprn(int ukprn)
        {
            //using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/providers/{ukprn}"))
            //{
            //    request.Headers.Add("Accept", "application/json");

            //    using (var response = _httpClient.SendAsync(request))
            //    {
            //        var result = response.Result;
            //        if (result.StatusCode == HttpStatusCode.OK)
            //        {
            //            return JsonConvert.DeserializeObject<Provider>(result.Content.ReadAsStringAsync().Result,
            //                _jsonSettings);
            //        }
            //        if (result.StatusCode == HttpStatusCode.NotFound)
            //        {
            //            RaiseResponseError($"The provider {ukprn} could not be found", request, result);
            //        }

            //        RaiseResponseError(request, result);
            //    }

            //    return null;
            //}

            throw new NotImplementedException();
        }

        public async Task<DateTime?> GetLatestNonOnboardingOrganisationChangeDate()
        {
            //return DateTime.Now;

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/download/roatp-summary/most-recent"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return JsonConvert.DeserializeObject<DateTime?>(result.Content.ReadAsStringAsync().Result,
                                _jsonSettings);
                        case HttpStatusCode.NotFound:
                            RaiseResponseError($"The most recent organisation update could not be found", request, result);
                            break;
                    }

                    RaiseResponseError(request, result);
                }

                return null;
            }
        }
    }
}
