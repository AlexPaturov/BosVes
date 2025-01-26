namespace BosVesAppLibrary.Command;

public class VagonsService
{
   private readonly GpriData _dbOneGpri;
   private readonly VagonsAPIData _dbVagonsAPI;
   private readonly InsertQueueService _insertQueue;
   private readonly CommandInvoker _commandInvoker;

   public VagonsService(GpriData dbOneGpri, VagonsAPIData dbVagonsAPI, InsertQueueService insertQueue)
   {
      _dbOneGpri = dbOneGpri;
      _dbVagonsAPI = dbVagonsAPI;
      _insertQueue = insertQueue;
      _commandInvoker = new CommandInvoker();
   }

   public async Task<bool> SaveVagons(GpriModel vagonsOne)
   {
      var command = new WriteVagonsCommand(vagonsOne, _dbOneGpri, _dbVagonsAPI, _insertQueue);
      return await _commandInvoker.ExecuteCommandAsync(command);
   }
}
