using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;
// Для подстановки брутто как тары на 9-х, ... ?


namespace BosVesAppLibrary.DataAccess;
public class BruttoAsTaraData
{
   private readonly string _connectionString;

   public BruttoAsTaraData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   // не переделывал согласно условию
   public async Task<IEnumerable<BruttoAsTaraModel>> GetByRangeDtAndNvag(string dtbegin, string dtend, string nvag, string vikno)
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT " +                                     //
                              "DV.DT AS DT" +                        //
                              ", DV.VR AS VR" +                      //
                              ", DV.NVAG AS NVAG" +                  //
                              ", DV.GRUZ AS GRUZ" +                  //
                              ", DV.BRUTTO AS BRUTTO" +              //
                              ", DV.TAR_BRS AS TAR_BRS" +            //
                              ", DV.TAR_DOK AS TAR_DOK" +            //
                              ", DV.NETTO AS NETTO" +                //
                              ", DV.NET_DOK AS NET_DOK" +            //
                              ", DV.CEX AS CEX" +                    //
                              ", DV.POTR AS POTR" +                  //
                              ", DV.VESY AS VESY" +                  //
                              ", DV.TN AS TN" +                      //
                              ", DV.NPP AS NPP" +                    //
                              ", DV.Id AS Id" +                      //
                              ", KC.NAIM AS NAIM " +                 //
                      "FROM GPRI DV " +                              //
                      "LEFT JOIN KCEX_GD KC on DV.CEX = KC.CEX " +   //
                      "WHERE DT BETWEEN @DTBEGIN AND @DTEND " +      //
                      "AND NVAG = @NVAG " +                          //
                      "AND (DV.BRUTTO > 0) " +                       //
                      "ORDER BY DV.DT, DV.VR, DV.NPP";               //

         var parameters = new { DTBEGIN = dtbegin, DTEND = dtend, NVAG = nvag };
         return await connection.QueryAsync<BruttoAsTaraModel>(query, parameters);
      }
   }

}
