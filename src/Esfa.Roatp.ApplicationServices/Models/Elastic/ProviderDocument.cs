using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Esfa.Roatp.ApplicationServices.Models.Elastic
{
    public class ProviderDocument
    {
        public long Ukprn { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProviderType ProviderType { get; set; }

        public bool ContractedForNonLeviedEmployers { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? ApplicationDeterminedDate { get; set; }
        public bool CurrentlyNotStartingNewApprentices { get; set; }

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
