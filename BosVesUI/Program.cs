using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;

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

            var app = builder.Build();

            // Step 3: Log information (example)
            //logger.Info("Blazor app started!");
           
            app.UseHsts();
            app.UseExceptionHandler("/Error");
            
            app.UseHttpsRedirection();
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
            //throw;
         }
         finally
         {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
         }
      }
   }
}