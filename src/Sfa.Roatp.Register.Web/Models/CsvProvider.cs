using System;

namespace Sfa.Roatp.Register.Web.Models
{
    public class CsvProvider
    {
        public long Ukprn { get; set; }

        public string ProviderType { get; set; }


        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}