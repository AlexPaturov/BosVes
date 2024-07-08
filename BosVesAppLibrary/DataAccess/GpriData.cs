using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class GpriData
{
   private readonly string _connectionString;

   public GpriData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
   }

   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task InsNew(GpriModel vagon)  // прооверен
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
                                       ",KOD_SAP " +
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
                             ",@KOD_SAP " +
                             ",@N_TEPLOVOZ " +
                             ",@POGRESHNOST " +
                             ",@REJVZVESH " +
                             ",@PLATFORMS_TARA " +
                             ",@PLATFORMS_BRUTTO " +
                             ",@ID_PLATFORMS " +
                             ",@SHABLON" +
                             ")";
         
         try
         {
            await connection.ExecuteAsync(query, vagon);
         }
         catch (Exception ex) 
         { 
            Console.WriteLine(ex.ToString());
         }
      }
   }

   public async Task UpdVag(GpriModel vagon)
   {
      using (var connection = CreateConnection())
      {
         var query = "UPDATE gpri set DT = @DT " +
                                    ",VR = @VR " +
                                    ",NVAG = @NVAG " +
                                    ",NDOK = @NDOK " +
                                    ",GRUZ = @GRUZ " +
                                    ",BRUTTO = @BRUTTO " +
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
                                    ",NPP = @NPP " +
                                    ",V13 = @V13 " +
                                    ",V24 = @V24 " +
                                    ",V12 = @V12 " +
                                    ",V34 = @V34 " +
                                    ",PP = @PP " +
                                    ",PR = @PR " +
                                    ",DELTA = @DELTA " +
                                    ",DATPER = @DATPER " +
                                    ",DT1 = @DT1 " +   // убрать ? где формируется
                                    ",VR1 = @VR1 " +   // убрать ? где формируется
                                    ",KOD_SAP = @KOD_SAP " +
                                    ",N_TEPLOVOZ = @N_TEPLOVOZ " +
                                    ",VR_PRV = @VR_PRV " +
                                    ",POGRESHNOST = @POGRESHNOST " +
                                    ",REJVZVESH = @REJVZVESH " +
                                    ",PLATFORMS_TARA = @PLATFORMS_TARA " +
                                    ",PLATFORMS_BRUTTO = @PLATFORMS_BRUTTO " +
                                    ",ID_PLATFORMS = @ID_PLATFORMS " +
                                    "SHABLON = @SHABLON " +
                     "where id = @ID";   
         await connection.ExecuteAsync(query, vagon);
      }
   }

   public async Task<IEnumerable<GpriModel>> GetByDtVr(string pDt, string pVr, string vikno) // в работе
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
                           ",KOD_SAP " +           //
                           ",N_TEPLOVOZ " +        //
                           ",VR_PRV " +            //
                           ",POGRESHNOST " +       //
                           ",REJVZVESH " +         //
                           ",ID " +                //
                           ",PLATFORMS_TARA " +    //
                           ",PLATFORMS_BRUTTO " +  //
                           ",ID_PLATFORMS " +      //
                     "FROM gpri " +                //
                     "where DT = @DT " +           //
                     "and VR = @VR " +             //
                     "and VESY = @VESY";           //

         var parameters = new { DT = pDt, VR = pVr, VESY = vikno };
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
                              ",KOD_SAP " +
                              ",N_TEPLOVOZ " +
                              ",VR_PRV " +
                              ",POGRESHNOST " +
                              ",REJVZVESH " +
                              ",ID " +
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
