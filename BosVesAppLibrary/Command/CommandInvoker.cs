
namespace BosVesAppLibrary.Command;

/// <summary>
/// The Invoker handles the execution and rollback logic:
/// </summary>
public class CommandInvoker
{
   public async Task<bool> ExecuteCommandAsync(ICommand command)
   {
      if (await command.Execute())
      {
         return true;
      }
      else
      {
         await command.Undo(); // Rollback if the command fails
         return false;
      }
   }
}
