using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using SFA.Roatp.Api.Types;

namespace SFA.Roatp.Api.Client
{
    public interface IRoatpServiceApiClient
    {
        //Task<IEnumerable<IDictionary<string, object>>> GetRoatpSummary();

        //Task<IEnumerable<IDictionary<string, object>>> GetRoatpSummaryUkprn(int ukprn);
        Task<List<Provider>> GetRoatpSummary();
        Task<Provider> GetRoatpSummaryUkprn(int ukprn);
        Task<DateTime?> GetLatestNonOnboardingOrganisationChangeDate();
    }
}
