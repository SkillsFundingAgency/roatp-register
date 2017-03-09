using Nest;

namespace Sfa.Roatp.Register.Infrastructure.Elasticsearch
{
    public interface IElasticsearchClientFactory
    {
        IElasticClient Create();
    }
}
