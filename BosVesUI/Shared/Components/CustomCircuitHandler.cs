using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BosVesUI.Shared.Components;

public class CustomCircuitHandler : CircuitHandler
{
   //private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

   public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
   {
     // logger.Info("Connection established.");
      return Task.CompletedTask;
   }

   public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
   {
     // logger.Warn("Connection lost.");
      return Task.CompletedTask;
   }
}
