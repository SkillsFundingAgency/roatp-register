using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Nest;
using Sfa.Roatp.Register.Infrastructure.Logging;
using SFA.DAS.NLog.Logger;

namespace Sfa.Roatp.Register.Infrastructure.Elasticsearch
{
    public class ElasticsearchCustomClient : IElasticsearchCustomClient
    {
        private readonly IElasticsearchClientFactory _elasticsearchClientFactory;

        private readonly ILog _logger;

        public ElasticsearchCustomClient(
            IElasticsearchClientFactory elasticsearchClientFactory,
            ILog logger)
        {
            _elasticsearchClientFactory = elasticsearchClientFactory;
            _logger = logger;
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector, [CallerMemberName] string callerName = "")
            where T : class
        {
            var client = _elasticsearchClientFactory.Create();
            var stopwatch = Stopwatch.StartNew();
            var result = client.Search(selector);

            SendLog<T>(result, $"Elasticsearch.Search.{callerName}", stopwatch.Elapsed);
            return result;
        }

        public ICountResponse Count<T>(Func<CountDescriptor<T>, ICountRequest> selector, [CallerMemberName] string callerName = "")
            where T : class
        {
            var client = _elasticsearchClientFactory.Create();
            var stopwatch = Stopwatch.StartNew();
            var result = client.Count(selector);

            SendLog<T>(result, $"Elasticsearch.Count.{callerName}", stopwatch.Elapsed);
            return result;
        }

        private void SendLog<T>(IResponse result, string identifier, TimeSpan clientRequestTime)
            where T : class
        {
            if (result is ISearchResponse<T>)
            {
                var searchResp = (ISearchResponse<T>) result;
                var body = string.Empty;
                if (result.ApiCall.RequestBodyInBytes != null)
                {
                    body = System.Text.Encoding.Default.GetString(result.ApiCall.RequestBodyInBytes);
                }

                var logEntry = new ElasticSearchLogEntry
                {
                    Identifier = identifier,
                    ReturnCode = result.ApiCall?.HttpStatusCode,
                    Successful = result.ApiCall?.Success,
                    SearchTime = searchResp.Took,
                    RequestTime = Math.Round(clientRequestTime.TotalMilliseconds, 2),
                    MaxScore = searchResp.MaxScore,
                    HitCount = searchResp.Hits?.Count(),
                    Url = result.ApiCall?.Uri?.AbsoluteUri,
                    Body = body
                };

                _logger.Debug("Elastic Search Requested", logEntry);
            }

            var dependencyLogEntry = new DependencyLogEntry
            {
                Identifier = identifier,
                ResponseCode = result.ApiCall?.HttpStatusCode,
                ResponseTime = Math.Round(clientRequestTime.TotalMilliseconds, 2),
                Url = result.ApiCall?.Uri?.AbsoluteUri
            };
            
            _logger.Debug("Dependency Elasticsearch", dependencyLogEntry);
        }
    }
}
