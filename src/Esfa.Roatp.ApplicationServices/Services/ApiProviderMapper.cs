using Esfa.Roatp.ApplicationServices.Models.Elastic;

namespace Esfa.Roatp.ApplicationServices.Services
{
    using ApiProvider = SFA.Roatp.Api.Types.Provider;
    using ApiType = SFA.Roatp.Api.Types.ProviderType;
    

    public static class ApiProviderMapper
    {
        public static ApiProvider Map(ProviderDocument source)
        {
            return new ApiProvider
            {
                Ukprn = source.Ukprn,
                //ContractedForNonLeviedEmployers = source.ContractedForNonLeviedEmployers,
                NewOrganisationWithoutFinancialTrackRecord = source.NewOrganisationWithoutFinancialTrackRecord,
                ParentCompanyGuarantee = source.ParentCompanyGuarantee,
                ProviderType = (ApiType) source.ProviderType,
                StartDate = source.StartDate,
                RefreshDate = source.RefreshDate,
                Name = source.Name,
	            CurrentlyNotStartingNewApprentices = source.CurrentlyNotStartingNewApprentices
            };
        }
    }
}