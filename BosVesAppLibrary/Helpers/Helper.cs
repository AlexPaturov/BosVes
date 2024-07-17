namespace BosVesAppLibrary.Helpers;
public static class Helper
{
   private static BosVesAppSettings _mySettings;

   //public static string GetConnFb(IOptions<BosVesAppSettings> mySettings)
   //{
   //   return  mySettings.Value.FbDbConnectionString;
   //}

   //public static string GetConnStringVal(string name) 
   //  {
   //      return ConfigurationManager.ConnectionStrings[name].ConnectionString;
   //  }

   public static string CorrectStr(string aStr)
   {
      return "'" + aStr + "'";
   }
}
