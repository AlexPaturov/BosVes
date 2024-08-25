using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;



namespace BosVesUI
{
   public class Program
   {
      public static void Main(string[] args)
      {
         // Early init of NLog to allow startup and exception logging, before host is built
         var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
         logger.Info("Start program");

         try
         {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
               app.UseExceptionHandler("/Error");
               app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            //logger.Info("info main");
            app.Run();
         }
         catch (Exception exception)
         {
            // NLog: catch setup errors
            logger.Error(exception, "Stopped program because of exception");
            //throw;
         }
         finally
         {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
         }
      }
   }
}