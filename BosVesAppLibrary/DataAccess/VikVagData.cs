using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BosVesAppLibrary.DataAccess;
public class VikVagData
{
   private readonly string _connectionString;
   
   public VikVagData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task<IEnumerable<VikVagModel>> GetByDate(string pDate, string pVikno) 
   {
      using (var connection = CreateConnection())
      {
         var query = "select [id] " +                          // 0
                           ",[vikno] " +	                     // 1
                           ",[datetime] " +					      // 2
                           ",[datetime_controller] " +		   // 3
                           ",[brutto] " + 						   // 4
                           ",[direct] " +						      // 5			
                           ",[processed] " +					      // 6
                           ",[npp] " +						         // 7
                           ",[type] " +						      // 8
                           ",[kol_os] " +						      // 9
                           ",[speed] " +						      // 10
                           ",[bogie1] " +						      // 11
                           ",[bogie2] " +						      // 12
                           ",[bogie1r1] " +					      // 13
                           ",[bogie1r2] " +					      // 14
                           ",[bogie2r1] " +					      // 15
                           ",[bogie2r2] " +					      // 16
                           ",[shift_pop] " +					      // 17
                           ",[shift_pro] " +					      // 18
                           ",[nvag_z] " +						      // 19
                           ",[nvag_v] " +						      // 20
                           ",[nvag_e] " +						      // 21
                           ",[video_id] " +					      // 22
                           ",[stat1] " +						      // 23
                           ",[stat2] " +						      // 24
                           ",[kalibrovka] " +					   // 25
                           ",[kalibrovka2] " +				      // 26
                           ",[train_id] " +					      // 27
                           ",[datper] " +						      // 28
                           ",[wtype] " +						      // 29
                           ",[datew1] " +						      // 30
                           ",[prim] " +					         // 31 // ещё добавиться 2 параметра
                     "from VIK_VAGS " +
                     "where vikno = @Vikno " +
                     "and datetime = @Date";

         var parameters = new { Vikno = pVikno, Date = pDate };
         
         return await connection.QueryAsync<VikVagModel>(query, parameters);
      }
   }

   // При удачной передаче в историческую базу - устанавливаем время передачи в централизованной.
   // Наличие времени в поле datper говорит о том что данная запись уже была передана и использовать для повторной передачи эту запись нельзя.
   public async Task SetUsedDate(int pId, DateTime pDateUsed) 
   {
      using (var connection = CreateConnection())
      {
         var query = "update VIK_VAGS set datper = @PDateUsed " +
                     "where id = @PId";
         var parameters = new { PDateUsed = pDateUsed, PId = pId };
         await connection.ExecuteAsync(query, parameters);
      }
   }




}
