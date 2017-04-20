using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sfa.Roatp.Register.Web.Helpers
{
    public static class HttpResponseFactory
    {
        public static HttpResponseException RaiseException(HttpStatusCode statusCode, string reason)
        {
            return new HttpResponseException(new HttpResponseMessage(statusCode)
            {
                ReasonPhrase = reason,
                Content = new StringContent(reason)
            });
        }
    }
}