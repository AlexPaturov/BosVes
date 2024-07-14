using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
   public async Task<IEnumerable<BruttoAsTaraModel>> GetByDtVrNvag(string pDt, string pVr, string nvag, string vikno)
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT " +
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
                              ",CEX " +
                              ",TARIF " +
                              ",POTR " +
                              ",PLAT " +
                              ",VESY " +
                              ",TN " +
                              ",N_TEPLOVOZ " +
                              ",POGRESHNOST " +
                              ",REJVZVESH " +
                              ",ID " +
                              ",PLATFORMS_TARA " +
                              ",PLATFORMS_BRUTTO " +
                              ",ID_PLATFORMS " +
                      "FROM gpri " +
                      "where DT betweem @DTBEGIN AND @DTEND " + // переделать на between
                      "and VESY = @VESY " + // ? нужно оставлять ?
                      "and NVAG = @NVAG";

         var parameters = new { DT = pDt, VR = pVr, NVAG = nvag, VESY = vikno };
         return await connection.QueryAsync<BruttoAsTaraModel>(query, parameters);
      }
   }

}
