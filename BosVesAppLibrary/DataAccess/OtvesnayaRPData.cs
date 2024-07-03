using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class OtvesnayaRPData
{
   private readonly string _connectionString;

   public OtvesnayaRPData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   //public List<OtvesnayaRP> GetByDtVikno();
   //public List<OtvesnayaRP> GetByDtVrVikno(string dtVr, string vikno);
   //public List<OtvesnayaRP> GetByDtGruzVikno(string dt, string gruz, string vikno);
   //public List<OtvesnayaRP> GetByDtVrGruVikno(string dtVr, string gruz, string vikno);
   //public List<OtvesnayaRP> GetByDtVrPostavVikno(string dtVr, string postav, string vikno);
}
