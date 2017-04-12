using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Core.Services;
using SFA.DAS.NLog.Logger;
using SFA.ROATP.Types;

namespace Sfa.Roatp.Register.Infrastructure.Elasticsearch
{
    public sealed class ProviderRepository : IGetProviders
    {
        private readonly IElasticsearchCustomClient _elasticsearchCustomClient;
        private readonly IConfigurationSettings _applicationSettings;
        private readonly ILog _log;
        private const string ProviderDocumentType = "roatpproviderdocument";

        public ProviderRepository(
            IElasticsearchCustomClient elasticsearchCustomClient,
            IConfigurationSettings applicationSettings,
            ILog log)
        {
            _elasticsearchCustomClient = elasticsearchCustomClient;
            _applicationSettings = applicationSettings;
            _log = log;
        }

        public IEnumerable<RoatpProvider> GetAllProviders()
        {
            var take = GetProvidersTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<RoatpProvider>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse(ProviderDocumentType))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.Ukprn))
                        .Take(take)
                        .MatchAll());

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException("Failed query all providers");
            }

            return results.Documents;
        }

        public RoatpProvider GetProvider(long ukprn)
        {
            var take = GetProvidersTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<RoatpProvider>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse(ProviderDocumentType))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.Ukprn))
                        .Take(take)
                        .Query(q => q
                            .Terms(t => t
                                .Field(f => f.Ukprn)
                                .Terms(ukprn))));

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException("Failed query all providers");
            }

            if (results.Documents.Count() > 1)
            {
                _log.Warn($"found {results.Documents.Count()} providers for the ukprn {ukprn}");
            }

            return results.Documents.FirstOrDefault();
        }

        private int GetProvidersTotalAmount()
        {
            var results =
                _elasticsearchCustomClient.Count<RoatpProvider>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse(ProviderDocumentType)));
            return (int) results.Count;
        }
    }
}
