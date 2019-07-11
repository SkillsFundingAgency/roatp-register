
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Sfa.Roatp.Register.Core.Configuration;
using SFA.Roatp.Api.Types;
using ProviderType = SFA.Roatp.Api.Types.ProviderType;

namespace SFA.Roatp.Api.Client
{
    public class RoatpServiceApiClient : IRoatpServiceApiClient //ApiClientBase

    { 
        // private readonly IConfigurationSettings _applicationSettings;

        //public RoatpServiceApiClient(IConfigurationSettings applicationSettings): base("https://roatp.apprenticeships.sfa.bis.gov.uk")
        //{
        //    _applicationSettings = applicationSettings;

        //    _httpClient.BaseAddress = new Uri(_applicationSettings.RoatpApiBaseUrl) ;
        //    // _applicationSettings = applicationSettings;
        //    //_applicationSettings = applicationSettings;
        //    // private ConfigurationSettings _applicationSettings;
        //}

        public async Task<List<Provider>> GetRoatpSummary()
        {
            var providers = new List<Provider>
            {
                new Provider
                {
                    Ukprn = 10061462,
                    Name = "AAA TRAINING SOLUTIONS LIMITED",
                    ProviderType = ProviderType.MainProvider,
                    ParentCompanyGuarantee = false,
                    NewOrganisationWithoutFinancialTrackRecord = false,
                    StartDate = new DateTime(2017, 05, 17),
                    ApplicationDeterminedDate = null
                },
                new Provider
                {
                    Ukprn = 10046498,
                    Name = "1ST CARE TRAINING LIMITED",
                    ProviderType = ProviderType.MainProvider,
                    ParentCompanyGuarantee = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    StartDate = new DateTime(2017, 03, 13),
                    ApplicationDeterminedDate = new DateTime(2019, 06, 01)
                }
                ,
                new Provider
                {
                    Ukprn = 10001236,
                    Name = "CBT SOLUTIONS LIMITED. T/A ewfwer",
                    ProviderType = ProviderType.EmployerProvider,
                    ParentCompanyGuarantee = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    StartDate = new DateTime(2017, 03, 13),
                    ApplicationDeterminedDate = new DateTime(2019, 06, 01)
                }
            };




            return providers;
        }

        public async Task<Provider> GetRoatpSummaryUkprn(int ukprn)
        {

            var provider = new Provider
            {
                Ukprn = ukprn,
                Name = "AAA TRAINING SOLUTIONS LIMITED",
                ProviderType = ProviderType.MainProvider,
                ParentCompanyGuarantee = true,
                NewOrganisationWithoutFinancialTrackRecord = false,
                StartDate = new DateTime(2017, 05, 17),
                ApplicationDeterminedDate = null
            };
            return provider;

        }

        public async Task<DateTime?> GetLatestNonOnboardingOrganisationChangeDate()
        {
            return DateTime.Now;

            //using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/download/roatp-summary/most-recent"))
            //{
            //    request.Headers.Add("Accept", "application/json");

            //    using (var response = _httpClient.SendAsync(request))
            //    {
            //        var result = response.Result;
            //        switch (result.StatusCode)
            //        {
            //            case HttpStatusCode.OK:
            //                return JsonConvert.DeserializeObject<DateTime?>(result.Content.ReadAsStringAsync().Result,
            //                    _jsonSettings);
            //            case HttpStatusCode.NotFound:
            //                RaiseResponseError($"The most recent organisation update could not be found", request, result);
            //                break;
            //        }

            //        RaiseResponseError(request, result);
            //    }

            //    return null;
            }

       
    }
    }
