using BosVesAppLibrary.Command;
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
      // NLog: Setup NLog for Dependency injection
      builder.Logging.ClearProviders();
      builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
      builder.Host.UseNLog();


      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddScoped<CircuitHandler, CustomCircuitHandler>();

      builder.Services.AddMemoryCache();
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
   }
}
