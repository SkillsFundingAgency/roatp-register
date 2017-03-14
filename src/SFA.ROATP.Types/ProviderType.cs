using System.ComponentModel;

namespace SFA.ROATP.Types
{
    public enum ProviderType
    {
        Unknown = 0,
        [Description("Main Provider")]
        MainProvider = 1,
        [Description("Supporting Provider")]
        SupportingProvider = 2,
        [Description("Employer Provider")]
        EmployerProvider = 3
    }
}
