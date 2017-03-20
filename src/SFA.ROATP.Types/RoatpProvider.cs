using System;
using System.Collections.Generic;

namespace SFA.ROATP.Types
{
    public class RoatpProvider
    {
        public long Ukprn { get; set; }

        [Obsolete("This value shouldn't be trusted as it should come from UKRLP")]
        public string Name { get; set; }

        public ProviderType ProviderType { get; set; }

        public bool ContractedForNonLeviedEmployers { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Obsolete("This is a temporary field")]
        public bool IsTransposed { get; set; }
    }
}
