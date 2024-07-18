using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection.PortableExecutable;

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

   // Выборка для 2-х весов, если был шаблон - ограничиваем диапазоном 21 день.
   public async Task<IEnumerable<TaraPodstanovkaModel>> GetTwentyOneDays(string vagnom) // не проверен
   {
      using (var connection = CreateConnection()) // Не сделел запрос как в рабочих весовых программах (нужны все 6-ть таблиц + agl32)
      {
         var query = "";

         var parameters = new { DTBEGIN = DateTime.Today.ToString("yyyy-MM-dd"), DTEND = DateTime.Today.AddDays(-21).ToString("yyyy-MM-dd"), VAGNOM = vagnom };
         return await connection.QueryAsync<TaraPodstanovkaModel>(query, parameters);
      }
   }

   // Для остальных весов - выборка без ограничений.
   public async Task<IEnumerable<TaraPodstanovkaModel>> GetFromYear(DateTime dtBegin, DateTime dtEnd, string vagnom) // не проверен
   {
      //if (dtBegin <  DateTime.Today.AddDays(365)) //  Сделать проверку на - (не больше года назад).
      //{ 
      //}

      using (var connection = CreateConnection()) // не сделел запрос как в целевых по (нужны все 6-ть таблиц)
      {

         //var sql = "select * from GET_TARA (@IN_NVAG, @IN_DTBEGIN, @IN_DTEND);";
         //var values = new { IN_NVAG = vagnom, IN_DTBEGIN = dtBegin.ToString("yyyy-MM-dd"), IN_DTEND = dtEnd.ToString("yyyy-MM-dd") };
         //var results = await connection.q<IEnumerable<TaraPodstanovkaModel>>(sql, values);
         //return results;

         //Set up DynamicParameters object to pass parameters  
         DynamicParameters parameters = new DynamicParameters();
         parameters.Add("IN_NVAG", vagnom);
         parameters.Add("IN_DTBEGIN", dtBegin);
         parameters.Add("IN_DTEND", dtEnd);

         //Execute stored procedure and map the returned result to a Customer object  
         var results = connection.QueryMultiple("GET_TARA", parameters, commandType: CommandType.StoredProcedure);
         var userdetails = results.Read<TaraPodstanovkaModel>().ToList(); // instead of dynamic, you can use your objects

         return userdetails;
         //var salarydetails = reader.Read<dynamic>().ToList();
      }
   }
}

