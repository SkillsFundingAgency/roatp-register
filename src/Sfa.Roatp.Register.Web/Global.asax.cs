using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Sfa.Roatp.Register.Web;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;

namespace Sfa.Roatp.Registry.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            TelemetryConfiguration.Active.InstrumentationKey = CloudConfigurationManager.GetSetting("InstrumentationKey") ?? string.Empty;
        }
    }
}