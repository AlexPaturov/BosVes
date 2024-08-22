using BosVesUI.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

      var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", false, true)
     .Build();

      var host = new WebHostBuilder()
     .UseConfiguration(configuration)
     .UseKestrel();

      // NLog: Setup NLog for Dependency injection
      builder.Logging.ClearProviders();
      builder.Host.UseNLog();

      // Add services to the container.
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
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

   }
}
