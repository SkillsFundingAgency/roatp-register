using System.Collections.Generic;
using SFA.Roatp.Api.Types;

namespace Sfa.Roatp.Register.Core.Services
{
    public interface IGetProviders
    {
        IEnumerable<RoatpProvider> GetAllProviders();
        RoatpProvider GetProvider(long ukprn);
    }
}
