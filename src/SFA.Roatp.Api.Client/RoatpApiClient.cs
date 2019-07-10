using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.Roatp.Api.Types;
using SFA.Roatp.Api.Types.Exceptions;

namespace SFA.Roatp.Api.Client
{
    public class RoatpApiClient : ApiClientBase, IRoatpClient
    {
        /// <summary>
        /// The constructor to optional set the api url for testing
        /// </summary>
        /// <param name="baseUri">ie: https://roatp.apprenticeships.sfa.bis.gov.uk</param>
        //[Obsolete("This is constructor used for testing upcoming versions of the API")]
        public RoatpApiClient(string baseUri) : base(baseUri)
        {
        }

        /// <summary>
        /// The default constructor to connect to https://roatp.apprenticeships.sfa.bis.gov.uk
        /// </summary>
        public RoatpApiClient() : base("https://roatp.apprenticeships.sfa.bis.gov.uk")
        {
        }


        /// <summary>
        /// Get a provider details
        /// GET /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>a provider details based on ukprn</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public Provider Get(string ukprn)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/providers/{ukprn}"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<Provider>(result.Content.ReadAsStringAsync().Result,
                            _jsonSettings);
                    }
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        RaiseResponseError($"The provider {ukprn} could not be found", request, result);
                    }

                    RaiseResponseError(request, result);
                }

                return null;
            }
        }

        /// <summary>
        /// Get a provider details
        /// GET /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>a provider details based on ukprn</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public Provider Get(long ukprn)
        {
            return Get(ukprn.ToString());
        }

        /// <summary>
        /// Get a provider details
        /// GET /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>a provider details based on ukprn</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public Provider Get(int ukprn)
        {
            return Get(ukprn.ToString());
        }

        /// <summary>
        /// Check if a provider exists
        /// HEAD /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>bool</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public bool Exists(string ukprn)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Head, $"/api/providers/{ukprn}"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.NoContent)
                    {
                        return true;
                    }
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        return false;
                    }

                    RaiseResponseError("Unexpected exception", request, result);
                }

                return false;
            }
        }

        /// <summary>
        /// Check if a provider exists
        /// HEAD /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>bool</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public bool Exists(int ukprn)
        {
            return Exists(ukprn.ToString());
        }


        /// <summary>
        /// Check if a provider exists
        /// HEAD /providers/{ukprn}
        /// </summary>
        /// <param name="ukprn">the provider ukprn this should be 8 numbers long</param>
        /// <returns>bool</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public bool Exists(long ukprn)
        {
            return Exists(ukprn.ToString());
        }

        /// <summary>
        /// Get a list of active providers on ROATP
        /// GET /providers
        /// </summary>
        /// <returns>Active providers</returns>
        /// <exception cref="EntityNotFoundException">when the resource you requested doesn't exist</exception>
        /// <exception cref="HttpRequestException">There was an unexpected error</exception>
        public IEnumerable<Provider> FindAll()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/api/providers"))
            {
                request.Headers.Add("Accept", "application/json");

                using (var response = _httpClient.SendAsync(request))
                {
                    var result = response.Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<Provider>>(result.Content.ReadAsStringAsync().Result, _jsonSettings);
                    }

                    RaiseResponseError(request, result);
                }

                return null;
            }
        }
    }
}
