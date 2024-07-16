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

   public async Task<IEnumerable<GruzGdModel>> GetAll(DateTime dtBegin, DateTime dtEnd, string vagnom, string vikno) // не проверен
   {
      using (var connection = CreateConnection()) // не сделел запрос как в целевых по (нужны все 6-ть таблиц)
      {
         // Для соблюдения правил СБ по ограничению выборки тары для 2-х весов, реализовано 2 варианта запроса:
         //    1) без ограничений в диапазоне времени;
         //    2) с ограничением диапазона выборки в 21 день если на вагон есть шаблон со станции примыкания. 

         string query = string.Empty;

         if (true) // если это 2-е весы и шаблон есть - ограничиваем выборку в 21 день 
         {
            query = "SELECT DT, VR, NVAG, BRUTTO, TAR_BRS, NETTO, GRUZ, VESY, ID " +
                     "FROM (select DT, VR, NVAG, BRUTTO, TAR_BRS, NETTO, GRUZ, VESY, ID " +
                        "from GPRI " +
                           "where (NVAG = @VAGNOM) and " +
                           "(TAR_BRS is not null) and " +
                           "(TAR_BRS > 0) and " +
                           "DT between @DTBEGIN and @DTEND " +
                        "union " +
                        "select DT, VR, NVAG, BRUTTO, TAR_BRS, NETTO, GRUZ, VESY, ID " +
                        "from gras " +
                        "where (NVAG = @VAGNOM) and " +
                           "(TAR_BRS is not null) and " +
                           "(TAR_BRS > 0) and " +
                           "DT between @DTBEGIN and @DTEND " +
                        "union " +
                        "select DT, VR, NVAG, BRUTTO, TAR_BRS, NETTO, GRUZ, 6 as " + Helper.CorrectStr(@columnName)  +", ID " +

                        ")";
         }
         else // иначе - без ограничений
         { 
         
         }

         
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
