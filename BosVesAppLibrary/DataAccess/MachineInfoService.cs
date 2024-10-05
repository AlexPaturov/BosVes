namespace BosVesAppLibrary.DataAccess;

public class MachineInfoService
{
   public string GetPCDomainName()
   {
      return Environment.UserDomainName; // Returns the domain name of the user's PC
   }

   public string GetMachineName()
   {
      return Environment.MachineName; // Returns the machine name
   }
}
