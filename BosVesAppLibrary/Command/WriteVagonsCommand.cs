namespace BosVesAppLibrary.Command;

public class WriteVagonsCommand : ICommand
{
   private readonly GpriModel _vagonsOne;
   private readonly GpriData _dbOneGpri;
   private readonly VagonsAPIData _dbVagonsAPI;
   private int _storedId;

   public WriteVagonsCommand(GpriModel vagonsOne, GpriData dbOneGpri, VagonsAPIData dbVagonsAPI)
   {
      _vagonsOne = vagonsOne;          // Вагон, который нужно сохранить. 
      _dbOneGpri = dbOneGpri;
      _dbVagonsAPI = dbVagonsAPI;      // вызываю UPDATE в базе ЦВИК
   }

   public async Task<bool> Execute()
   {
      // Try to write to databaseOne
      _storedId = await _dbOneGpri.InsNew(_vagonsOne);
      if (_storedId == -1)
      {
         return false; // If the writeOne fails, return false
      }

      // Set VagonsAPI properties after successful writeOne
      var _vagonsAPI = new VagonsAPI
      {
         Id = _storedId,
         LastUpdatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name, // Replace with actual user data
         LastUpdateDate = DateTime.Now
      };

      // Try to write to databaseAPI
      // Что может вернуть операция обновления ?
      if ( 1 != await _dbVagonsAPI.UpdVag(_vagonsAPI)) // Переделать на bool?
      {
         await Undo(); // Rollback the insertion into databaseOne
         return false;
      }

      return true;
   }

   public async Task Undo()
   {
      if (_storedId > 0)
      {
         await _dbOneGpri.Delete(_storedId);  // Delete the record from databaseOne
      }
   }
}
