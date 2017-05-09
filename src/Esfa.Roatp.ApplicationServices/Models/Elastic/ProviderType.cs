using System.ComponentModel;

namespace Esfa.Roatp.ApplicationServices.Models.Elastic
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
