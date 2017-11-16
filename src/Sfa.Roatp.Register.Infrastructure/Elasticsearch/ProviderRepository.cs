using System;
using System.Collections.Generic;
using System.Linq;
using Esfa.Roatp.ApplicationServices.Models;
using Esfa.Roatp.ApplicationServices.Models.Elastic;
using Esfa.Roatp.ApplicationServices.Services;
using Nest;
using Sfa.Roatp.Register.Core.Configuration;
using SFA.DAS.NLog.Logger;
using SFA.Roatp.Api.Types;

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

        public IEnumerable<ProviderDocument> GetAllProviders()
        {
            var take = GetProvidersTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<ProviderDocument>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse(ProviderDocumentType))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.Ukprn))
                        .Take(take)
                        .MatchAll());

            if (results.ApiCall.HttpStatusCode != 200)
            {
                _log.Warn($"httpStatusCode was {results.ApiCall.HttpStatusCode}");
                throw new ApplicationException("Failed query all providers");
            }

            return results.Documents;
        }

        public ProviderDocument GetProvider(int ukprn)
        {
            var take = GetProvidersTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<ProviderDocument>(
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
                _log.Warn($"httpStatusCode was {results.ApiCall.HttpStatusCode}");
                throw new ApplicationException($"Failed query provider with ukprn: {ukprn}");
            }

            if (results.Documents.Count() > 1)
            {
                _log.Warn($"found {results.Documents.Count()} providers for the ukprn {ukprn}");
            }

            return results.Documents.FirstOrDefault();
        }

        public DateTime GetDateOfProviderList()
        {
            var index = _elasticsearchCustomClient.GetIndicesPointingToAlias(_applicationSettings.RoatpProviderIndexAlias).FirstOrDefault();
            return IndexUtility.GetDateFromIndexNameAndDateExtension(index, _applicationSettings.RoatpProviderIndexAlias);
        }

        private int GetProvidersTotalAmount()
        {
            var results =
                _elasticsearchCustomClient.Count<Provider>(
                    s =>
                    s.Index(_applicationSettings.RoatpProviderIndexAlias)
                        .Type(Types.Parse(ProviderDocumentType)));
            return (int) results.Count;
        }
    }
}
