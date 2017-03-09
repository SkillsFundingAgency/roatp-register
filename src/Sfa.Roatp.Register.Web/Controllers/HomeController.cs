using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using CsvHelper;
using Sfa.Roatp.Register.Core.Configuration;
using Sfa.Roatp.Register.Core.Models;
using Sfa.Roatp.Register.Core.Services;
using Sfa.Roatp.Register.Infrastructure;

namespace Sfa.Roatp.Register.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetProviders _getProviders;
        private readonly IConfigurationSettings _settings;

        public HomeController(IGetProviders getProviders, IConfigurationSettings settings)
        {
            _getProviders = getProviders;
            _settings = settings;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 86400)]
        public ContentResult RobotsText()
        {
            var builder = new StringBuilder();

            builder.AppendLine("User-agent: *");

            if (!_settings.EnvironmentName.Equals("Prod", StringComparison.OrdinalIgnoreCase))
            {
                builder.AppendLine("Disallow: /");
            }
            else if (new RobotsAllowFeature().FeatureEnabled == false)
            {
                builder.AppendLine("Disallow: /");
            }

            return Content(builder.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}