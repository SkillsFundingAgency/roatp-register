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

        public string ElasticsearchUsername => CloudConfigurationManager.GetSetting("ElasticSearch.Username");

        public string ElasticsearchPassword => CloudConfigurationManager.GetSetting("ElasticSearch.Password");

        public bool EnableES5 => CloudConfigurationManager.GetSetting("EnableES5") == "true";

        public bool IgnoreSslCertificateEnabled => CloudConfigurationManager.GetSetting("FeatureToggle.IgnoreSslCertificateFeature") == "true";

        public string RoatpApiBaseUrl => CloudConfigurationManager.GetSetting("RoatpApiBaseUrl");

        private IEnumerable<Uri> GetElasticSearchIps()
        {
            var urlStrings = CloudConfigurationManager.GetSetting("ElasticServerUrls").Split(',');
            return urlStrings.Select(url => new Uri(url));
        }
    }
}
