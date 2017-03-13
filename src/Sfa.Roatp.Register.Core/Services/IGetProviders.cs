using System.Collections.Generic;
using Sfa.Roatp.Register.Core.Models;
using SFA.ROATP.Types;

namespace Sfa.Roatp.Register.Core.Services
{
    public interface IGetProviders
    {
        IEnumerable<RoatpProvider> GetAllProviders();
    }
}
