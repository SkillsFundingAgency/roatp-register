using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SFA.Roatp.Api.Types
{
    public class Provider
    {
        public long Ukprn { get; set; }

        public string Uri { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProviderType ProviderType { get; set; }

        //public bool ContractedForNonLeviedEmployers { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsDateValid(DateTime currentDate)
        {
            return StartDate.HasValue && StartDate.Value.Date <= currentDate.Date;
        }
    }
}
