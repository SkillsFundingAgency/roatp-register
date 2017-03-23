using System;
using Sfa.Roatp.Register.Infrastructure.Utility;
using SFA.ROATP.Types;

namespace Sfa.Roatp.Register.Web.Models
{
    public static class CsvProviderMapper
    {
        public static CsvProvider Map(RoatpProvider provider)
        {
            var csvProvider = new CsvProvider
            {
                Ukprn = provider.Ukprn,
                Name = provider.Name,
                ProviderType = Enumerations.GetEnumDescription(provider.ProviderType),
                NewOrganisationWithoutFinancialTrackRecord = provider.NewOrganisationWithoutFinancialTrackRecord,
                ParentCompanyGuarantee = provider.ParentCompanyGuarantee,
                StartDate = FormatDate(provider.StartDate),
                EndDate = FormatDate(provider.EndDate)
            };

            return csvProvider;
        }

        private static string FormatDate(DateTime? date)
        {
            return date?.ToString("dd/MM/yyyy") ?? string.Empty;
        }
    }
}