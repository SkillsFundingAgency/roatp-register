using System.Collections.Generic;
using Sfa.Roatp.Register.Core.Models;

namespace Sfa.Roatp.Register.Core.Services
{
    public interface IGetProviders
    {
        IEnumerable<RoatpProvider> GetAllProviders();
    }
}
