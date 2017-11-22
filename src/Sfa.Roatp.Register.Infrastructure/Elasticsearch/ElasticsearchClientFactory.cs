using System.Security.Cryptography.X509Certificates;

using Elasticsearch.Net;
using Nest;

using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Infrastructure.Extensions;

using SFA.Roatp.Api.Types;

namespace Sfa.Roatp.Register.Infrastructure.Elasticsearch
{
    public sealed class ElasticsearchClientFactory : IElasticsearchClientFactory
    {
        private readonly IConfigurationSettings _applicationSettings;

        public ElasticsearchClientFactory(IConfigurationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public IElasticClient Create()
        {
            ConnectionSettings settings;
            if (_applicationSettings.IgnoreSslCertificateEnabled)
            {
                settings = new ConnectionSettings(new StaticConnectionPool(_applicationSettings.ElasticServerUrls), new MyCertificateIgnoringHttpConnection());
            }
            else
            {
                settings = new ConnectionSettings(new StaticConnectionPool(_applicationSettings.ElasticServerUrls));
            }

            settings.DisableDirectStreaming();
            settings.MapDefaultTypeNames(d => d.Add(typeof(Provider), "roatpproviderdocument"));

            if (_applicationSettings.EnableES5)
            {
                settings.BasicAuthentication(_applicationSettings.ElasticsearchUsername, _applicationSettings.ElasticsearchPassword);
            }

            using (settings)
            {
                return new ElasticClient(settings);
            }
        }
    }
}
