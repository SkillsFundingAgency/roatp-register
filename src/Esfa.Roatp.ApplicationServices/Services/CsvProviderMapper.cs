using System;
using Esfa.Roatp.ApplicationServices.Models;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Esfa.Roatp.ApplicationServices.Utility;
using SFA.Roatp.Api.Types;
using Provider = Esfa.Roatp.ApplicationServices.Models.CsvReport.Provider;

namespace Sfa.Roatp.Register.Web.Models
{
    public static class CsvProviderMapper
    {
        public static Provider Map(ProviderDocument providerDocument)
        {
            var csvProvider = new Provider
            {
                Ukprn = providerDocument.Ukprn,
                Name = providerDocument.Name,
                ProviderType = Enumerations.GetEnumDescription(providerDocument.ProviderType),
                NewOrganisationWithoutFinancialTrackRecord = providerDocument.NewOrganisationWithoutFinancialTrackRecord,
                ParentCompanyGuarantee = providerDocument.ParentCompanyGuarantee
            };

            return csvProvider;
        }

        private static string FormatDate(DateTime? date)
        {
            return date?.ToString("dd/MM/yyyy") ?? string.Empty;
        }
    }
}