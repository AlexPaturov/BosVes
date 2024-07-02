using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class GruzGdData
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

   public async Task InsNew(GruzGdModel gruz)                                       // не проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "insert into gruz_gd(GRUZIK, KOD_SAP, NAME_SAP, NAME_OZM) values (@GRUZIK, @KOD_SAP, @NAME_SAP, @NAME_OZM)";
         await connection.ExecuteAsync(query, gruz);
      }
   }

   public async Task UpdGruz(GruzGdModel gruz)                                      // не проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "update gruz_gd set GRUZIK = @GRUZIK, "  +
                                        "KOD_SAP = @KOD_SAP, "  +
                                        "NAME_SAP = @NAME_SAP, "  +
                                        "NAME_OZM = @NAME_OZM"; // не поставил matching 
         await connection.ExecuteAsync(query, gruz);
      }
   }

   public async Task<IEnumerable<GruzGdModel>> GetNameByPartName(string name)       // не проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "select GRUZIK, KOD_SAP, NAME_SAP, NAME_OZM from gruz_gd where GRUZIK like %@name%"; // не отладил LIKE
         return await connection.QueryAsync<GruzGdModel>(query);
      }
   }

   public async Task<IEnumerable<GruzGdModel>> GetAll()                             // проверено
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT GRUZIK, KOD_SAP, NAME_SAP, NAME_OZM FROM gruz_gd";
         return await connection.QueryAsync<GruzGdModel>(query);
      }
   }

 
}
