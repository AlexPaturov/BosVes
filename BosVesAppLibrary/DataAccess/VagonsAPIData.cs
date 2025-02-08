using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;

public class VagonsAPIData
{
   private readonly string _connectionString;
   private readonly ILogger<VagonsAPI> _logger;

   public VagonsAPIData(IOptions<BosVesAppSettings> mySettings, ILogger<VagonsAPI> logger)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
      _logger = logger;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task<int> UpdVag(VagonsAPI vagon)
   {
      using (var connection = CreateConnection())
      {
         var query = "UPDATE or insert into VAGONSAPI (LASTUPDATEDBY, LASTUPDATEDATE, ID) " +
                     "values (@lastupdatedby, @lastupdateddate, @id) " +
                     "matching(ID)";

         var parameters = new { lastupdatedby = vagon.LastUpdatedBy, lastupdateddate = vagon.LastUpdateDate, id = vagon.Id };
         var res = await connection.ExecuteAsync(query, parameters);
         return res;
      }
   }
}
