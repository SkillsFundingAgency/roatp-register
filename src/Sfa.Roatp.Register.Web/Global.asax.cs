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
        private ILog _logger;

        public Global()
        {
            _logger = DependencyResolver.Current.GetService<ILog>();
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            MvcHandler.DisableMvcResponseHeader = true;
            var logger = DependencyResolver.Current.GetService<ILog>();

            logger.Info("Starting Web Role");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            logger.Info("Web Role started");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            application?.Context?.Response.Headers.Remove("Server");

            _logger = DependencyResolver.Current.GetService<ILog>();

            HttpContext context = base.Context;
            if (!context.Request.Path.StartsWith("/__browserlink"))
            {
                _logger.Info($"{context.Request.HttpMethod} {context.Request.Path}");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            var logger = DependencyResolver.Current.GetService<ILog>();
            
            logger.Error(ex, "App_Error");
        }

    }
}