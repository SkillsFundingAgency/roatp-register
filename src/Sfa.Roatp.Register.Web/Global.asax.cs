using System;
using System.Web;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;
using SFA.DAS.NLog.Logger;

namespace Sfa.Roatp.Registry.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            MvcHandler.DisableMvcResponseHeader = true;
            var logger = DependencyResolver.Current.GetService<ILog>();

            logger.Info("Starting Web Role");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SetupApplicationInsights();

            logger.Info("Web Role started");
        }

        private bool UrlContains(string text)
        {
            var url = HttpContext.Current.Request.RequestContext.HttpContext.Request.Url;
            if (url == null)
            {
                return false;
            }

            return url.OriginalString.Contains(text);
        }

        private void SetupApplicationInsights()
        {
            TelemetryConfiguration.Active.InstrumentationKey = CloudConfigurationManager.GetSetting("InstrumentationKey");

            TelemetryConfiguration.Active.TelemetryInitializers.Add(new ApplicationInsightsInitializer());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            var logger = DependencyResolver.Current.GetService<ILog>();
            
            logger.Error(ex, "App_Error");
        }

    }
}