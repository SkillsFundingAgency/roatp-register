using System.Collections.Generic;
using SFA.Roatp.Api.Types;

namespace SFA.Roatp.Api.Client
{
    public interface IRoatpClient
    {
        Provider Get(string providerUkprn);
        Provider Get(long providerUkprn);
        Provider Get(int providerUkprn);
        bool Exists(string providerUkprn);
        IEnumerable<Provider> FindAll();
    }
}