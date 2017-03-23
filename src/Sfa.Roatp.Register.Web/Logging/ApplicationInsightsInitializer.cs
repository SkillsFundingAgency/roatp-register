using Microsoft.ApplicationInsights.Channel;

namespace Sfa.Roatp.Registry.Web
{
    public sealed class ApplicationInsightsInitializer : Microsoft.ApplicationInsights.Extensibility.ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Properties["Application"] = "Sfa.Roatp.Web";
        }
    }
}