namespace Esfa.Roatp.ApplicationServices.Models.CsvReport
{
    public class Provider
    {
        public long Ukprn { get; set; }

        public string Name { get; set; }

        public string ProviderType { get; set; }

        public bool ParentCompanyGuarantee { get; set; }

        public bool NewOrganisationWithoutFinancialTrackRecord { get; set; }

        public string StartDate { get; set; }

        public string ProviderNotCurrentlyStartingNewApprentices { get; set; }
        public string ApplicationDeterminedDate { get; set; }
    }
}