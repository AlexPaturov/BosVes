using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Runtime.ConstrainedExecution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BosVesAppLibrary.DataAccess;
public class KcexGdData
{
   private readonly string _connectionString;

   public KcexGdData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task InsNew(KcexGdModel cex)
   {
      using (var connection = CreateConnection())
      {
         var query = "INSERT INTO KCEX_GD(CEX, NAIM) values (@CEX, @NAIM)";
         await connection.ExecuteAsync(query, cex);
      }
   }

   public async Task UpdGruz(KcexGdModel cex)
   {
      using (var connection = CreateConnection())
      {
         var query = "UPDATE KCEX_GD set NAIM = @NAIM where CEX = @CEX";
         await connection.ExecuteAsync(query, cex);
      }
   }

   public async Task<IEnumerable<KcexGdModel>> GetAll()
   {
      using (var connection = CreateConnection())
      {
         var query = "select CEX, NAIM from KCEX_GD order by CEX";
         return await connection.QueryAsync<KcexGdModel>(query);
      }
   }

   // сделать класс - справочник
   //public async Task<IEnumerable<KcexGdModel>> GetNumByCex(string cexName) 
   //{ 
   //}
   //public List<Kcex_gd> GetCexByNum(string cexNum);

}
