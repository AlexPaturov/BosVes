using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;


namespace BosVesAppLibrary.DataAccess;
public class GpriData
{
   private readonly string _connectionString;
   private readonly ILogger<GpriData> _logger;

   public GpriData(IOptions<BosVesAppSettings> mySettings, ILogger<GpriData> logger)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
      _logger = logger;
   }

   public IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task<int> InsNew(GpriModel vagon)  // прооверен
   {
      
      using (var connection = CreateConnection()) 
      {
         var query = "insert into gpri (" +
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
                                       ",MUSOR " +
                                       ",CEX " +
                                       ",TARIF " +
                                       ",POTR " +
                                       ",PLAT " +
                                       ",SKOR " +
                                       ",VESY " +
                                       ",TN " +
                                       ",NPP " +
                                       ",V13 " +
                                       ",V24 " +
                                       ",V12 " +
                                       ",V34 " +
                                       ",PP " +
                                       ",PR " +
                                       ",DELTA " +
                                       ",N_TEPLOVOZ " +
                                       ",POGRESHNOST " +
                                       ",REJVZVESH " +
                                       ",PLATFORMS_TARA " +
                                       ",PLATFORMS_BRUTTO " +
                                       ",ID_PLATFORMS " +
                                       ",SHABLON " +
                                       ") " +
                      "values (" +
                             "@DT " +
                             ",@VR " +
                             ",@NVAG " +
                             ",@NDOK " +
                             ",@GRUZ " +
                             ",@BRUTTO " +
                             ",@TAR_BRS " +
                             ",@TAR_DOK " +
                             ",@NETTO " +
                             ",@NET_DOK " +
                             ",@MUSOR " +
                             ",@CEX " +
                             ",@TARIF " +
                             ",@POTR " +
                             ",@PLAT " +
                             ",@SKOR " +
                             ",@VESY " +
                             ",@TN " +
                             ",@NPP " +
                             ",@V13 " +
                             ",@V24 " +
                             ",@V12 " +
                             ",@V34 " +
                             ",@PP " +
                             ",@PR " +
                             ",@DELTA " +
                             ",@N_TEPLOVOZ " +
                             ",@POGRESHNOST " +
                             ",@REJVZVESH " +
                             ",@PLATFORMS_TARA " +
                             ",@PLATFORMS_BRUTTO " +
                             ",@ID_PLATFORMS " +
                             ",@SHABLON" +
                             ") RETURNING Id";
         
         
           return await connection.ExecuteScalarAsync<int>(query, vagon);
        
      }
   }

   // проверяем, существует ли запись в базе ?
   public async Task<int?> СheckExisting(GpriModel vagon) 
   {
      using var connection = CreateConnection();
      string query = "select ID from gpri where DT = @DT AND VR = @VR AND NVAG = @NVAG AND VESY = @VESY";
      var parameters = new { DT = vagon.DT.Date.ToShortDateString(), VR = vagon.VR, NVAG = vagon.NVAG, VESY = vagon.VESY };

      return await connection.ExecuteScalarAsync<int?>(query, parameters);
   }

   public int UpdVag(GpriModel vagon)                   
   {
      using var connection = CreateConnection();
      connection.Open();
      using var transection = connection.BeginTransaction(IsolationLevel.ReadCommitted);   

               var query = "UPDATE gpri set DT = @DT " +
                                    ",VR = @VR " +
                                    ",NVAG = @NVAG " +
                                    ",NDOK = @NDOK " +
                                    ",GRUZ = @GRUZ " +
                                    ",TAR_BRS = @TAR_BRS " +
                                    ",TAR_DOK = @TAR_DOK " +
                                    ",NETTO = @NETTO " +
                                    ",NET_DOK = @NET_DOK " +
                                    ",MUSOR = @MUSOR " +
                                    ",CEX = @CEX " +
                                    ",TARIF = @TARIF " +
                                    ",POTR = @POTR " +
                                    ",PLAT = @PLAT " +
                                    ",SKOR = @SKOR " +
                                    ",VESY = @VESY " +
                                    ",TN = @TN " +
                                    ",V13 = @V13 " +
                                    ",V24 = @V24 " +
                                    ",V12 = @V12 " +
                                    ",V34 = @V34 " +
                                    ",PP = @PP " +
                                    ",PR = @PR " +
                                    ",DELTA = @DELTA " +
                                    ",N_TEPLOVOZ = @N_TEPLOVOZ " +
                                    ",POGRESHNOST = @POGRESHNOST " +
                                    ",REJVZVESH = @REJVZVESH " +
                                    ",PLATFORMS_TARA = @PLATFORMS_TARA " +
                                    ",PLATFORMS_BRUTTO = @PLATFORMS_BRUTTO " +
                                    ",ID_PLATFORMS = @ID_PLATFORMS " +
                                    ",SHABLON = @SHABLON " + 
                     "where id = @Id " + 
                     "returning id";
      try
      {
         var res = connection.ExecuteScalar<int>(query, vagon, transection, 30);
         transection.Commit();
         return res;
      }
      catch (Exception ex) 
      {
         transection?.Rollback();
         Console.WriteLine(ex.ToString());
         return -1;
      }
   }

   // Используется при неудачной попытке установки маркера в цвик web api.
   public async Task<int> Delete(int id)                                
   {
      using (var connection = CreateConnection())
      {
         var query = "DELETE FROM gpri where id = @Id";
         return await connection.ExecuteAsync(query, new { ID = id });
      }
   }

   public async Task<IEnumerable<GpriModel>> GetByDtVr(string pDt, string pVr, string vikno) // прооверен
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT " +                   //
                           "DT " +                 //
                           ",VR " +                //
                           ",NVAG " +              //
                           ",NDOK " +              //
                           ",GRUZ " +              //
                           ",BRUTTO " +            //
                           ",TAR_BRS " +           //
                           ",TAR_DOK " +           //
                           ",NETTO " +             //
                           ",NET_DOK " +           //
                           ",MUSOR " +             //
                           ",CEX " +               //
                           ",TARIF " +             //
                           ",POTR " +              //
                           ",PLAT " +              //
                           ",SKOR " +              //
                           ",VESY " +              //
                           ",TN " +                //
                           ",NPP " +               //
                           ",V13 " +               //
                           ",V24 " +               //
                           ",V12 " +               //
                           ",V34 " +               //
                           ",PP " +                //
                           ",PR " +                //
                           ",DELTA " +             //
                           ",N_TEPLOVOZ " +        //
                           ",POGRESHNOST " +       //
                           ",REJVZVESH " +         //
                           ",Id " +                //
                           ",PLATFORMS_TARA " +    //
                           ",PLATFORMS_BRUTTO " +  //
                           ",ID_PLATFORMS " +      //
                           //",SHABLON " +      //
                     "FROM gpri " +                //
                     "where DT = @DT " +           //
                     "and VR = @VR " +             //
                     "and VESY = @VESY";           //

         var parameters = new { DT = pDt, VR = pVr, VESY = vikno };
         return await connection.QueryAsync<GpriModel>(query, parameters);
      }
   }

   public async Task<IEnumerable<GpriModel>> GetByDt(string pDt, string vikno) // прооверен
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT " +                   //
                           "DT " +                 //
                           ",VR " +                //
                           ",NVAG " +              //
                           ",NDOK " +              //
                           ",GRUZ " +              //
                           ",BRUTTO " +            //
                           ",TAR_BRS " +           //
                           ",TAR_DOK " +           //
                           ",NETTO " +             //
                           ",NET_DOK " +           //
                           ",MUSOR " +             //
                           ",CEX " +               //
                           ",TARIF " +             //
                           ",POTR " +              //
                           ",PLAT " +              //
                           ",SKOR " +              //
                           ",VESY " +              //
                           ",TN " +                //
                           ",NPP " +               //
                           ",V13 " +               //
                           ",V24 " +               //
                           ",V12 " +               //
                           ",V34 " +               //
                           ",PP " +                //
                           ",PR " +                //
                           ",DELTA " +             //
                           ",N_TEPLOVOZ " +        //
                           ",POGRESHNOST " +       //
                           ",REJVZVESH " +         //
                           ",ID " +                //
                           ",PLATFORMS_TARA " +    //
                           ",PLATFORMS_BRUTTO " +  //
                           ",ID_PLATFORMS " +      //
                     "FROM gpri " +                //
                     "where DT = @DT " +           //
                     "and VESY = @VESY";           //

         var parameters = new { DT = pDt, VESY = vikno };
         return await connection.QueryAsync<GpriModel>(query, parameters);
      }
   }

   public async Task<IEnumerable<GpriModel>> GetByDtVrNvag(string pDt, string pVr, string nvag, string vikno)
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
                              ",MUSOR " +
                              ",CEX " +
                              ",TARIF " +
                              ",POTR " +
                              ",PLAT " +
                              ",SKOR " +
                              ",VESY " +
                              ",TN " +
                              ",NPP " +
                              ",V13 " +
                              ",V24 " +
                              ",V12 " +
                              ",V34 " +
                              ",PP " +
                              ",PR " +
                              ",DELTA " +
                              ",N_TEPLOVOZ " +
                              ",POGRESHNOST " +
                              ",REJVZVESH " +
                              ",Id " +
                              ",PLATFORMS_TARA " +
                              ",PLATFORMS_BRUTTO " +
                              ",ID_PLATFORMS " +
                      "FROM gpri " +
                      "where DT = @DT " +
                      "and VR = @VR " +
                      "and VESY = @VESY " + 
                      "and NVAG = @NVAG";

         var parameters = new { DT = pDt, VR = pVr, NVAG = nvag, VESY = vikno };
         return await connection.QueryAsync<GpriModel>(query, parameters);
      }
   }

}
