using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using System.Globalization;

namespace BosVesUI
{
   public class Program
   {
      public static void Main(string[] args)
      {
         try
         {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();  // Remove other logging providers if you want only NLog
            builder.ConfigureServices();
            #region Set up the culture to "dd.MM.yyyy"
            var cultureInfo = new CultureInfo("en-GB") // 'en-GB' uses 'dd.MM.yyyy' format by default
            {
               DateTimeFormat = { ShortDatePattern = "dd.MM.yyyy", LongDatePattern = "dd.MM.yyyy HH:mm:ss" }
            };
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            #endregion
            var app = builder.Build();
            #region Middleware setup to the culture to "dd.MM.yyyy"
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
               DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
               SupportedCultures = new List<CultureInfo> { cultureInfo },
               SupportedUICultures = new List<CultureInfo> { cultureInfo }
            });
            #endregion
            
            // Step 3: Log information (example)
            //logger.Info("Blazor app started!");

            // app.UseHsts();
            app.UseExceptionHandler("/Error");
            app.MapControllers();
            // app.UseHttpsRedirection(); // попытка отключить https
            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // Step 4: Log a message during app lifetime events (optional)
            //app.Lifetime.ApplicationStarted.Register(() => logger.Info("Application has started."));
            //app.Lifetime.ApplicationStopped.Register(() => logger.Info("Application has stopped."));
            app.Run();
         }
         catch (Exception exception)
         {
            // NLog: catch setup errors
            Console.WriteLine(exception.StackTrace);
            //throw;
         }
         finally
         {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
         }
      }
   }
}