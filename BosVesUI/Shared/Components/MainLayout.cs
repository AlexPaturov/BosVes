using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BosVesUI.Shared.Components;

public partial class MainLayout : LayoutComponentBase
{
   // private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

   [JSInvokable]
   public static void LogWindowClose()
   {
      // Logger.Info("Browser window closed.");
   }
}
