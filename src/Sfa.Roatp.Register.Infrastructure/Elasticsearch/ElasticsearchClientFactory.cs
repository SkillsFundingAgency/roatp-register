using Elasticsearch.Net;
using Nest;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Core.Models;

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
            using (var settings = new ConnectionSettings(new StaticConnectionPool(_applicationSettings.ElasticServerUrls)))
            {
                settings.DisableDirectStreaming();
                settings.MapDefaultTypeNames(d => d.Add(typeof(RoatpProvider), "roatpproviderdocument"));

                return new ElasticClient(settings);
            }
        }
    }
}
