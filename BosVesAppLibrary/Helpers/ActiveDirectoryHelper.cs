using System.DirectoryServices.AccountManagement;

namespace BosVesAppLibrary.Helpers;
public class ActiveDirectoryHelper
{
   private readonly string domainName;

   public ActiveDirectoryHelper(string domainName)
   {
      this.domainName = domainName;
   }

   public bool IsUserInGroup(string userName, string groupName)
   {
      using (var context = new PrincipalContext(ContextType.Domain, domainName))
      {
         // Find the user in the domain
         var user = UserPrincipal.FindByIdentity(context, userName);
         if (user == null)
         {
            return false; // User not found
         }

         // Check if user is a member of the group
         return user.IsMemberOf(context, IdentityType.Name, groupName);
      }
   }

   public bool IsComputerInGroup(string computerName, string groupName)
   {
      using (var context = new PrincipalContext(ContextType.Domain, domainName))
      {
         // Find the computer in the domain
         var computer = ComputerPrincipal.FindByIdentity(context, computerName);
         if (computer == null)
         {
            return false; // Computer not found
         }

         // Check if the computer is a member of the group
         return computer.IsMemberOf(context, IdentityType.Name, groupName);
      }
   }
}
