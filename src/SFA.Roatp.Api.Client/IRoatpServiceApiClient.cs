using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.Roatp.Api.Client
{
    public interface IRoatpServiceApiClient
    {
        Task<IEnumerable<IDictionary<string, object>>> GetRoatpSummary();

        Task<IEnumerable<IDictionary<string, object>>> GetRoatpSummaryUkprn(int ukprn);
        Task<DateTime?> GetLatestNonOnboardingOrganisationChangeDate();
    }
}
