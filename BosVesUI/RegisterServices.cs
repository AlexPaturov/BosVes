namespace BosVesUI;

public static class RegisterServices
{
   public static void ConfigureServices(this WebApplicationBuilder builder) 
   {
      // Add services to the container.
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
   }
}
