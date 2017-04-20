using System.ComponentModel;

namespace SFA.Roatp.Api.Types
{
    public enum ProviderType
    {
        Unknown = 0,
        [Description("Main provider")]
        MainProvider = 1,
        [Description("Supporting provider")]
        SupportingProvider = 2,
        [Description("Employer provider")]
        EmployerProvider = 3
    }
}
