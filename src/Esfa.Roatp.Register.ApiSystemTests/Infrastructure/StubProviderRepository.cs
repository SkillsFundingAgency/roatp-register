using System;
using System.Collections.Generic;
using System.Linq;
using SFA.Roatp.Api.Types;
using Esfa.Roatp.ApplicationServices.Services;
using Esfa.Roatp.ApplicationServices.Models.Elastic;

namespace sfa.Roatp.Register.ApiIntegrationTests.Infrastructure
{
    public class StubProviderRepository : IGetProviders
    {
        public List<ProviderDocument> roatpProviderDocuments { get; set; }

        public StubProviderRepository()
        {
            // We can load default roatp provider data in to the repository if needed
            // roatpProviders = AdddefaultRoatpData();
            roatpProviderDocuments = new List<ProviderDocument>();
        }

        IEnumerable<ProviderDocument> IGetProviders.GetAllProviders()
        {
            return roatpProviderDocuments;
        }

        ProviderDocument IGetProviders.GetProvider(int ukprn)
        {
            return roatpProviderDocuments.FirstOrDefault(x => x.Ukprn == ukprn);
        }

        private List<ProviderDocument> AddDefaultRoatpData()
        {
            roatpProviderDocuments = new List<ProviderDocument>() { 
                new ProviderDocument
                {
                    Ukprn = 99992101,
                    Name = "EAST BERKSHIRE COLLEGE 1",
                    ProviderType = Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                },
                new ProviderDocument
                {
                    Ukprn = 99992102,
                    Name = "EAST BERKSHIRE COLLEGE 2",
                    ProviderType = Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                },
                new ProviderDocument
                {
                    Ukprn = 99992103,
                    Name = "EAST BERKSHIRE COLLEGE 3",
                    ProviderType = Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                },
                new ProviderDocument
                {
                    Ukprn = 99992104,
                    Name = "EAST BERKSHIRE COLLEGE 4",
                    ProviderType = Esfa.Roatp.ApplicationServices.Models.Elastic.ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                }
            };

            return roatpProviderDocuments.ToList();
        }

        public DateTime GetDateOfProviderList()
        {
            return DateTime.UtcNow;
        }
    }
}
