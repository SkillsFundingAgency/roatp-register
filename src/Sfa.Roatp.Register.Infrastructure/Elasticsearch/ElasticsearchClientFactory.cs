using System.Diagnostics;
using System.Linq;
using System.Net;
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
                settings = new ConnectionSettings(new SingleNodeConnectionPool(_applicationSettings.ElasticServerUrls.FirstOrDefault()), new MyCertificateIgnoringHttpConnection());
            }
            else
            {
                settings = new ConnectionSettings(new SingleNodeConnectionPool(_applicationSettings.ElasticServerUrls.FirstOrDefault()));
            }

            settings.DisableDirectStreaming();
            settings.DefaultMappingFor<Provider>(m => m
                .IndexName("roatpproviderdocument"));

            if (_applicationSettings.EnableES5)
            {
                settings.BasicAuthentication(_applicationSettings.ElasticsearchUsername, _applicationSettings.ElasticsearchPassword);
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (settings)
            {
                return new ElasticClient(settings);
            }
        }
    }
}
