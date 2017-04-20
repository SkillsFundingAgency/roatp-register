using System;
using System.Runtime.CompilerServices;
using Nest;

namespace Sfa.Roatp.Register.Infrastructure.Elasticsearch
{
    public interface IElasticsearchCustomClient
    {
        ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector, [CallerMemberName] string callerName = "")
            where T : class;

        ICountResponse Count<T>(Func<CountDescriptor<T>, ICountRequest> selector, [CallerMemberName] string callerName = "") 
            where T : class;
    }
}
