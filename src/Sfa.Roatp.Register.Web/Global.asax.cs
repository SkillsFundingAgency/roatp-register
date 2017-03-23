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

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            var logger = DependencyResolver.Current.GetService<SFA.DAS.NLog.Logger.ILog>();

            if (ex is HttpException
                && ((HttpException)ex).GetHttpCode() != 404
                && !UrlContains("findatrainingorganisation.nas.apprenticeships.org.uk"))
            {
                logger.Error(ex, "App_Error");
            }
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
    }
}