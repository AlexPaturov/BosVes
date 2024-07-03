using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class TaraPodstanovkaData
{
   private readonly string _connectionString;

   public TaraPodstanovkaData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task<IEnumerable<GruzGdModel>> GetAll(DateTime begin, DateTime end, string vagnom, string vikno) // не проверен
   {
      using (var connection = CreateConnection()) // не сделел запрос как в целевых по (нужны все 6-ть таблиц)
      {
         var query = "SELECT DT, VR, NVAG, BRUTTO, TAR_BRS, NETTO, VESY, ID " + 
                     "FROM gpri " +
                     "where DT beetwen @Begin and @And " +
                     "and VESY = @vikno " +
                     "and NVAG = @vikno";
         var parameters = new { Begin = begin, And = end, VESY = vikno, NVAG = vagnom };
         return await connection.QueryAsync<GruzGdModel>(query, parameters);
      }
   }

   // В зависимости от номера весов будут меняться правила для выборки списка вагонов
   // подходящих под ограничивающие условия для данных весов.
   private string GetQuery(DateTime begin, DateTime end, string vagnom, string vikno) 
   {
      return "";
   }

   private string GetParameters(DateTime begin, DateTime end, string vagnom, string vikno)
   {
      return "";
   }
}
