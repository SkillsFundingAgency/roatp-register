﻿using System;
using System.Collections.Generic;

namespace Sfa.Roatp.Register.Core.Configuration
{
    public interface IConfigurationSettings
    {
        string RoatpProviderIndexAlias { get; }

        IEnumerable<Uri> ElasticServerUrls { get; }

        string EnvironmentName { get; }


        string ElasticsearchUsername { get; }

        string ElasticsearchPassword { get;}

        bool EnableES5 { get; }

        string RoatpApiBaseUrl { get; }

        bool IgnoreSslCertificateEnabled { get; }
    }
}