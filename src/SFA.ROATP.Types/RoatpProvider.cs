using System;
using System.Collections.Generic;

namespace SFA.ROATP.Types
{
    public class RoatpProvider
    {
        public long Ukprn { get; set; }

        public ProviderType ProviderType { get; set; }

        public bool ContractedForNonLeviedEmployers { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
