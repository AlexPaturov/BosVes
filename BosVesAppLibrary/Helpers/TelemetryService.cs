using Microsoft.AspNetCore.Http;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace BosVesAppLibrary.Helpers;

public class TelemetryService
{
   private readonly ILogger _logger; private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly TelemetryClient _telemetryClient;

   public TelemetryService(IHttpContextAccessor httpContextAccessor, TelemetryClient telemetryClient)
   {
      _httpContextAccessor = httpContextAccessor;
      _telemetryClient = telemetryClient;
   }

   public void TrackCustomEvent(string eventName)
   {
      var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

      var telemetry = new EventTelemetry(eventName);
      telemetry.Properties.Add("ClientIP", ipAddress ?? "Unknown");
      _telemetryClient.TrackEvent(telemetry);
   }
}
