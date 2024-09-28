using BosVesAppLibrary.Command;
using BosVesUI.Shared.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using NLog;

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
      builder.Services.AddSingleton<GitVersionService>(); // читаю гит версию
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
