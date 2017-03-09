using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Azure;
using Sfa.Roatp.Register.Core.Configuration;

namespace Sfa.Roatp.Register.Infrastructure.Settings
{
    public sealed class ApplicationSettings : IConfigurationSettings
    {
        public string RoatpProviderIndexAlias => CloudConfigurationManager.GetSetting("RoatpProviderIndexAlias");

        public IEnumerable<Uri> ElasticServerUrls => GetElasticSearchIps();

        public string EnvironmentName => CloudConfigurationManager.GetSetting("EnvironmentName");

        public string ApplicationName => CloudConfigurationManager.GetSetting("ApplicationName");

        private IEnumerable<Uri> GetElasticSearchIps()
        {
            var urlStrings = CloudConfigurationManager.GetSetting("ElasticServerUrls").Split(',');
            return urlStrings.Select(url => new Uri(url));
        }
    }
}
