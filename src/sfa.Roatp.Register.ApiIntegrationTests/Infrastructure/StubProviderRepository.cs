using System;
using System.Collections.Generic;
using System.Linq;
using Sfa.Roatp.Register.Core.Services;
using SFA.Roatp.Api.Types;

namespace sfa.Roatp.Register.ApiIntegrationTests.Infrastructure
{
    public class StubProviderRepository : IGetProviders
    {
        public List<RoatpProvider> roatpProviders { get; set; }

        public StubProviderRepository()
        {
            // We can load default roatp provider data in to the repository if needed
            // roatpProviders = AdddefaultRoatpData();
            roatpProviders = new List<RoatpProvider>();
        }

        IEnumerable<RoatpProvider> IGetProviders.GetAllProviders()
        {
            return roatpProviders;
        }

        RoatpProvider IGetProviders.GetProvider(int ukprn)
        {
            return roatpProviders.FirstOrDefault(x => x.Ukprn == ukprn);
        }

        private List<RoatpProvider> AdddefaultRoatpData()
        {
            roatpProviders = new List<RoatpProvider>() { 
                new RoatpProvider
                {
                    Ukprn = 99992101,
                    Name = "EAST BERKSHIRE COLLEGE 1",
                    ProviderType = ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                },
                new RoatpProvider
                {
                    Ukprn = 99992102,
                    Name = "EAST BERKSHIRE COLLEGE 2",
                    ProviderType = ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                },
                new RoatpProvider
                {
                    Ukprn = 99992103,
                    Name = "EAST BERKSHIRE COLLEGE 3",
                    ProviderType = ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                },
                new RoatpProvider
                {
                    Ukprn = 99992104,
                    Name = "EAST BERKSHIRE COLLEGE 4",
                    ProviderType = ProviderType.MainProvider,
                    ContractedForNonLeviedEmployers = false,
                    NewOrganisationWithoutFinancialTrackRecord = true,
                    ParentCompanyGuarantee = true,
                    StartDate = DateTime.Now.AddDays(-100)
                }
            };

            return roatpProviders.ToList();
        }
    }
}
