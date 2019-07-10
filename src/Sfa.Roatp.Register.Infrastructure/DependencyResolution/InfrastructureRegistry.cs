using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using Esfa.Roatp.ApplicationServices.Services;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Infrastructure.Elasticsearch;
using Sfa.Roatp.Register.Infrastructure.Settings;
using SFA.DAS.NLog.Logger;
using SFA.Roatp.Api.Client;
using StructureMap;

namespace Sfa.Roatp.Register.Infrastructure.DependencyResolution
{
    public sealed class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<ILog>().Use(x => new NLogLogger(
                x.ParentType, 
                x.GetInstance<IRequestContext>(),
                GetProperties())).AlwaysUnique();
            For<IConfigurationSettings>().Use<ApplicationSettings>();
            For<IGetProviders>().Use<ProviderRepository>();
            
            For<IRoatpServiceApiClient>().Use<RoatpServiceApiClient>();
            //For<IRoatpApiClient>().Use<RoatpApiClient>();  // http://localhost:37951/
            For<IElasticsearchClientFactory>().Use<ElasticsearchClientFactory>();
            For<IElasticsearchCustomClient>().Use<ElasticsearchCustomClient>();
        }

        private IDictionary<string, object> GetProperties()
        {
            var properties = new Dictionary<string, object>();
            properties.Add("Version", GetVersion());
            return properties;
        }

        private string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}
