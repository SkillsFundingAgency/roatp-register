using System.Collections.Generic;
using Esfa.Roatp.ApplicationServices.Models;
using Esfa.Roatp.ApplicationServices.Models.Elastic;

namespace Esfa.Roatp.ApplicationServices.Services
{
    public interface IGetProviders
    {
        IEnumerable<ProviderDocument> GetAllProviders();
        ProviderDocument GetProvider(int ukprn);
    }
}
