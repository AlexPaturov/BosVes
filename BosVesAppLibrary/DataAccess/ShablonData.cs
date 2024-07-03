using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class ShablonData
{
   private readonly string _connectionString;

   public ShablonData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task Delete(ShablonModel vagon, string vikno)
   {
      using (var connection = CreateConnection())
      {
         var query = "DELETE FROM "+ GetShablonTableName(vikno) + 
                     " WHERE Id = @Id";
         await connection.ExecuteAsync(query, new { Id = vagon.ID });
      }
   }

   public async Task<IEnumerable<ShablonModel>> GetByNvag(string nvag, string vikno) // если больше 1-го – удалить
   {
      using (var connection = CreateConnection())
      {
         var query = "select " +
                           "DT " +
                           ",VR " +
                           ",NVAG " +
                           ",NDOK " +
                           ",GRUZ " +
                           ",BRUTTO " +
                           ",TAR_BRS " +
                           ",TAR_DOK " +
                           ",NETTO " +
                           ",NET_DOK " +
                           ",MUSOR " +
                           ",CEX " +
                           ",TARIF " +
                           ",POTR " +
                           ",PLAT " +
                           ",SKOR " +
                           ",VESY " +
                           ",TN " +
                           ",NPP " +
                           ",DATPAM " +
                           ",ID " +
                       "from " + GetShablonTableName(vikno) + " " +
                       "where NVAG = @NVAG";
         var parameters = new { NVAG = nvag };
         return await connection.QueryAsync<ShablonModel>(query, parameters);
      }
   }

   public async Task<IEnumerable<ShablonModel>> GetAll(string vikno) 
   {
      using (var connection = CreateConnection())
      {
         var query = "select " +
                          "DT " +
                          ",VR " +
                          ",NVAG " +
                          ",NDOK " +
                          ",GRUZ " +
                          ",BRUTTO " +
                          ",TAR_BRS " +
                          ",TAR_DOK " +
                          ",NETTO " +
                          ",NET_DOK " +
                          ",MUSOR " +
                          ",CEX " +
                          ",TARIF " +
                          ",POTR " +
                          ",PLAT " +
                          ",SKOR " +
                          ",VESY " +
                          ",TN " +
                          ",NPP " +
                          ",DATPAM " +
                          ",ID " +
                      "from " + GetShablonTableName(vikno);
         return await connection.QueryAsync<ShablonModel>(query);
      }
   }

   private string GetShablonTableName(string vikno) 
   {
      switch (vikno)
      {
         case "01":
            return "VESY1";
         case "02":
            return "VESY2";
         case "07":
            return "VESY7";
         case "09":
            return "VESY9";
         case "17":
            return "VESY17";
         default:
            return null;
      }
   }
}
