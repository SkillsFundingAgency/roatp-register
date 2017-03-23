using System;
using System.Collections.Generic;
using Nest;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Core.Services;
using SFA.ROATP.Types;

namespace Sfa.Roatp.Register.Infrastructure.Elasticsearch
{
    public sealed class ProviderRepository : IGetProviders
    {
        private readonly IElasticsearchCustomClient _elasticsearchCustomClient;
        private readonly IConfigurationSettings _applicationSettings;

        public ProviderRepository(
            IElasticsearchCustomClient elasticsearchCustomClient,
            IConfigurationSettings applicationSettings)
        {
            _elasticsearchCustomClient = elasticsearchCustomClient;
            _applicationSettings = applicationSettings;
        }

        public IEnumerable<RoatpProvider> GetAllProviders()
        {
            var take = GetProvidersTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<RoatpProvider>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse("roatpproviderdocument"))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.Ukprn))
                        .Take(take)
                        .MatchAll());

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException($"Failed query all providers");
            }

            return results.Documents;
        }

        private int GetProvidersTotalAmount()
        {
            var results =
                _elasticsearchCustomClient.Search<RoatpProvider>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse("roatpproviderdocument"))
                        .From(0)
                        .MatchAll());
            return (int)results.HitsMetaData.Total;
        }
    }
}
