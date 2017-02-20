using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Sfa.Roatp.Register.Web.Attributes
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //var logger = DependencyResolver.Current.GetService<ILog>();

            //logger.Error(context.Exception, $"App_Error {context.Request?.RequestUri}");
        }
    }
}