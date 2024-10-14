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
      builder.Logging.ClearProviders();
      builder.Logging.AddConsole();
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
      // ------------------------------------------------------------------------------------------------------------------------
      builder.Services.AddSingleton<XmlDecodeService>();  // служба хмл - декодер
      builder.Services.AddSingleton<UserService>();       // служба служит обработчиком и хранилищем информации о пользователе
      builder.Services.AddSingleton<TcpClientService>();  // служба получающая имя пк юзера по TCP от службы windows
      builder.Services.AddHostedService<CommandService>();// 
      //-------------------------------------------------------------------------------------------------------------------------
      builder.Services.Configure<ActiveDirectoryHelper>(builder.Configuration.GetSection("ActiveDirectory"));
      builder.Services.Configure<BosVesAppSettings>(builder.Configuration.GetSection("BosVesAppSettings")); // заполняю конфигурационными настройками классс, который планирую использовать во всём приложении
      builder.Services.AddSingleton<BosVesAppSettings>();            // 
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
