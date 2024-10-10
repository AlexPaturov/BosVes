using BosVesUI.Shared.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BosVesUI;

public static class RegisterServices
{
   public static void ConfigureServices(this WebApplicationBuilder builder) 
   {
      builder.Services.Configure<RequestLocalizationOptions>(options =>
      {
         var supportedCultures = new[] { "en-US", "ru", "uk" };
         options.SetDefaultCulture(supportedCultures[0])
             .AddSupportedCultures(supportedCultures)
             .AddSupportedUICultures(supportedCultures);
      });

      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddControllers();

      //-------------------------работаю с логами--------------------------------
      builder.Logging.AddApplicationInsights();
      builder.Services.AddApplicationInsightsTelemetry();

      // Register IHttpContextAccessor
      builder.Services.AddHttpClient();
      builder.Services.AddHttpContextAccessor();

      // Register TelemetryService
      //builder.Services.AddSingleton<TelemetryService>();
      //---------------------------------------------------------

      builder.Services.AddScoped<CircuitHandler, CustomCircuitHandler>();

      builder.Services.AddMemoryCache();
      // Register Configuration
      builder.Services.Configure<ActiveDirectoryHelper>(builder.Configuration.GetSection("ActiveDirectory"));
      builder.Services.Configure<BosVesAppSettings>(builder.Configuration.GetSection("BosVesAppSettings")); // заполняю конфигурационными настройками классс, который планирую использовать во всём приложении
      builder.Services.AddSingleton<GpriData>();            // 
      builder.Services.AddSingleton<GrasData>();            // 
      builder.Services.AddSingleton<GruzGdData>();          // 
      builder.Services.AddSingleton<KcexGdData>();          // 
      builder.Services.AddSingleton<OtvesnayaRPData>();     // 
      builder.Services.AddSingleton<ShablonData>();         // 
      builder.Services.AddSingleton<TaraPodstanovkaData>(); // 
      builder.Services.AddSingleton<VikVagData>();          // 
      builder.Services.AddSingleton<BruttoAsTaraData>();    // 
      builder.Services.AddSingleton<VagonsAPIData>();       // 
      builder.Services.AddSingleton<VagonsService>();       // 
      builder.Services.AddSingleton<MachineInfoService>(); // Register the service
                                                           // Configure the TCP client service with target IP and port
      builder.Services.AddSingleton<TcpClientService>();
   }
}
