using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BosVesUI
{
   public class Program
   {
      public static void Main(string[] args)
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
         app.Run();
      }
   }
}