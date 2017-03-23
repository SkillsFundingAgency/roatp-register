using System.Web;
using Sfa.Roatp.Register.Web.Logging;
using SFA.DAS.NLog.Logger;

namespace Sfa.Roatp.Register.Web.DependencyResolution
{
    public sealed class WebRegistry : StructureMap.Registry
    {
        public WebRegistry()
        {
            For<IRequestContext>().Use(x => new RequestContext(new HttpContextWrapper(HttpContext.Current)));
        }
    }
}