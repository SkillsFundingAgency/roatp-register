using System;

namespace SFA.Roatp.Api.Types
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

        public string Uri { get; set; }

        public bool IsDateValid(DateTime currentDate)
        {
            if (StartDate == null)
            {
                return false;
            }

            if (StartDate.Value.Date <= currentDate.Date && currentDate.Date <= EndDate)
            {
                return true;
            }

            return StartDate.Value.Date <= currentDate.Date && EndDate == null;
        }
    }
}
