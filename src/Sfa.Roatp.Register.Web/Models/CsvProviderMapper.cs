using System;
using Sfa.Roatp.Register.Infrastructure.Utility;
using SFA.ROATP.Types;

namespace Sfa.Roatp.Register.Web.Models
{
    public static class CsvProviderMapper
    {
        public static CsvProvider Map(RoatpProvider provider)
        {
            return new CsvProvider
            {
                Ukprn = provider.Ukprn,
                ProviderType = Enumerations.GetEnumDescription(provider.ProviderType),
                ContractedForNonLeviedEmployers = provider.ContractedForNonLeviedEmployers,
                NewOrganisationWithoutFinancialTrackRecord = provider.NewOrganisationWithoutFinancialTrackRecord,
                ParentCompanyGuarantee = provider.ParentCompanyGuarantee,
                StartDate = FormatDate(provider.StartDate),
                EndDate = FormatDate(provider.EndDate)
            };
        }

        private static string FormatDate(DateTime? date)
        {
            return date?.ToShortDateString() ?? string.Empty;
        }
    }
}