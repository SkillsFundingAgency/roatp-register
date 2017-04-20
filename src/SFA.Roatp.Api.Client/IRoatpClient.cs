using System.Collections.Generic;
using SFA.Roatp.Api.Types;

namespace SFA.Roatp.Api.Client
{
    public interface IRoatpClient
    {
        RoatpProvider Get(string providerUkprn);
        RoatpProvider Get(long providerUkprn);
        RoatpProvider Get(int providerUkprn);
        bool Exists(string providerUkprn);
        IEnumerable<RoatpProvider> FindAll();
    }
}