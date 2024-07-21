using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class GruzGdData : IGruzGdData
{
   private readonly string _connectionString;

   public GruzGdData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task InsNew(GruzGdModel gruz)                                       // проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "insert into gruz_gd(GRUZIK, KOD_SAP, NAME_SAP, NAME_OZM) values (@GRUZIK, @KOD_SAP, @NAME_SAP, @NAME_OZM)";
         await connection.ExecuteAsync(query, gruz);
      }
   }

   //public async Task UpdGruz(GruzGdModel gruz)                                      // проверено - не использовать пока не будет уникального идентификатора записи
   //{
   //   using (var connection = CreateConnection())
   //   {
   //      var query = "update gruz_gd set GRUZIK = @GRUZIK " +
   //                                     "where KOD_SAP = @KOD_SAP";                 // код сап не везде есть 
   //      await connection.ExecuteAsync(query, gruz);
   //   }
   //}

   public async Task<IEnumerable<GruzGdModel>> GetNameByPartName(string pName)       // проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "select GRUZIK, KOD_SAP, NAME_SAP, NAME_OZM from gruz_gd where GRUZIK like @name"; 
         var parameters = new { name = "%" + pName + "%" };
         return await connection.QueryAsync<GruzGdModel>(query, parameters);
      }
   }

   public async Task<IEnumerable<GruzGdModel>> GetAll()                             // проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT GRUZIK, KOD_SAP, NAME_SAP, NAME_OZM FROM gruz_gd order by GRUZIK";
         return await connection.QueryAsync<GruzGdModel>(query);
      }
   }


}
