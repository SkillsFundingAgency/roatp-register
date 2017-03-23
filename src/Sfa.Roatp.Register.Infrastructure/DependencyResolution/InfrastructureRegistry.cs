using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Elasticsearch;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Core.Services;
using Sfa.Roatp.Register.Infrastructure.Elasticsearch;
using Sfa.Roatp.Register.Infrastructure.Logging;
using Sfa.Roatp.Register.Infrastructure.Settings;
using SFA.DAS.NLog.Logger;
using StructureMap;

namespace Sfa.Roatp.Register.Infrastructure.DependencyResolution
{
    public sealed class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<ILog>().Use(x => new NLogLogger(x.ParentType, x.GetInstance<IRequestContext>())).AlwaysUnique();
            For<IConfigurationSettings>().Use<ApplicationSettings>();
            For<IGetProviders>().Use<ProviderRepository>();
            For<IElasticsearchClientFactory>().Use<ElasticsearchClientFactory>();
            For<IElasticsearchCustomClient>().Use<ElasticsearchCustomClient>();
        }
    }
}
