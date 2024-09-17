namespace BosVesAppLibrary.Command;

public class VagonsService
{
   private readonly GpriData _dbOneGpri;
   private readonly VagonsAPIData _dbVagonsAPI;
   private readonly CommandInvoker _commandInvoker;

   public VagonsService(GpriData dbOneGpri, VagonsAPIData dbVagonsAPI)
   {
      _dbOneGpri = dbOneGpri;
      _dbVagonsAPI = dbVagonsAPI;
      _commandInvoker = new CommandInvoker();
   }

   public async Task<bool> SaveVagons(GpriModel vagonsOne)
   {
      var command = new WriteVagonsCommand(vagonsOne, _dbOneGpri, _dbVagonsAPI);
      return await _commandInvoker.ExecuteCommandAsync(command);
   }
}
