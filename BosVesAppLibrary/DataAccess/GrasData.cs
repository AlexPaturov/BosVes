using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BosVesAppLibrary.DataAccess;
public class GrasData
{
   private readonly string _connectionString;
   private readonly string _vikno;

   public GrasData(IOptions<BosVesAppSettings> mySettings)
   {
      _connectionString = mySettings.Value.FbDbConnectionString;
      _vikno = mySettings.Value.Vikno; 
   }
   private IDbConnection CreateConnection()
   {
      return new FbConnection(_connectionString);
   }

   public async Task InsNew(List<GrasModel> vagon) 
   {
      using (var connection = CreateConnection())
      {
         var query = "INSERT INTO gras ("+
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
                                       //",VR_PRV " +
                                       ",POGRESHNOST " +
                                       ",REJVZVESH " +
                                       ",PLATFORMS_TARA " +
                                       ",PLATFORMS_BRUTTO " +
                                       ",ID_PLATFORMS " +
                                       ") " + 
                     "VALUES (" +
                              "@DT " +             // 1
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
                              //",@VR_PRV " +         // Забираю из ЦВИК
                              ",@POGRESHNOST " +
                              ",@REJVZVESH " +
                              ",@PLATFORMS_TARA " +
                              ",@PLATFORMS_BRUTTO " +
                              ",@ID_PLATFORMS " +
                              ")";
         await connection.ExecuteAsync(query, vagon);
      }
   }

   public async Task UpdVag(GrasModel vagon) 
   {
      using (var connection = CreateConnection())
      {
         var query = "UPDATE gras SET NVAG = @NVAG " +                           // 1
                                     ",NDOK = @NDOK " +                          //
                                     ",GRUZ = @GRUZ " +                          //
                                     //",BRUTTO = @BRUTTO " +                    // нужно ли обновлять брутто по технологии ? 
                                     ",TAR_BRS = @TAR_BRS " +                    // или когда нужно под тару подставить брутто ?
                                     ",TAR_DOK = @TAR_DOK " +                    //
                                     ",NETTO = @NETTO " +                        //
                                     ",NET_DOK = @NET_DOK " +                    //
                                     ",MUSOR = @MUSOR " +                        //
                                     ",CEX = @CEX " +                            //
                                     ",TARIF = @TARIF " +                        // 
                                     ",POTR = @POTR " +                          //
                                     ",PLAT = @PLAT " +                          //
                                     ",SKOR = @SKOR " +                          //
                                     ",VESY = @VESY " +                          //
                                     ",TN = @TN " +                              //
                                     //",NPP = @NPP " +                          //
                                     ",V13 = @V13 " +                            //
                                     ",V24 = @V24 " +                            //
                                     ",V12 = @V12 " +                            //
                                     ",V34 = @V34 " +                            //
                                     ",PP = @PP " +                              //
                                     ",PR = @PR " +                              //
                                     ",DELTA = @DELTA " +                        //
                                     ",N_TEPLOVOZ = @N_TEPLOVOZ " +              //
                                     //",VR_PRV = @VR_PRV " +                    //
                                     ",POGRESHNOST = @POGRESHNOST " +            //
                                     ",REJVZVESH = @REJVZVESH " +                //
                                     ",PLATFORMS_TARA = @PLATFORMS_TARA " +      //
                                     ",PLATFORMS_BRUTTO = @PLATFORMS_BRUTTO " +  //
                                     ",ID_PLATFORMS = @ID_PLATFORMS " +          //
                                     "WHERE Id = @Id";                           //
         await connection.ExecuteAsync(query, vagon);
      }
   }

   // Получаю список вагонов за указанную дату, время, номер весов 
   public async Task<IEnumerable<GrasModel>> GetByDtVr(string pDt, string pVr) 
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT " +                      //
                              "DT, " +                //
                              "VR, " +                //
                              "NVAG, " +              //
                              "NDOK, " +              //
                              "GRUZ, " +              //
                              "BRUTTO, " +            //
                              "TAR_BRS, " +           //
                              "TAR_DOK, " +           //
                              "NETTO, " +             //
                              "NET_DOK, " +           //
                              "MUSOR, " +             //
                              "CEX, " +               //
                              "TARIF, " +             //
                              "POTR, " +              //
                              "PLAT, " +              //
                              "SKOR, " +              //
                              "VESY, " +              //
                              "TN, " +                //
                              "NPP, " +               //
                              "V13, " +               //
                              "V24, " +               //
                              "V12, " +               //
                              "V34, " +               //
                              "PP, " +                //
                              "PR, " +                //
                              "DELTA, " +             //
                              "N_TEPLOVOZ, " +        //
                              //"VR_PRV," +           //
                              "POGRESHNOST, " +       //
                              "REJVZVESH, " +         //
                              "PLATFORMS_TARA, " +    //
                              "PLATFORMS_BRUTTO, " +  //
                              "Id, " +                //
                              "ID_PLATFORMS " +       //
                     "FROM gras " +                   //
                     "where DT = @DT " +              //
                     "and VR = @VR " +                //
                     "and VESY = @VESY";              //

         var parameters = new { DT = pDt, VR = pVr, VESY = _vikno };
         return await connection.QueryAsync<GrasModel>(query, parameters);
      }
   }

   // Получить 1 вагон для корректировки 
   public async Task<GrasModel> GetByDtVrNvag(string pDt, string pVr, string nvag)
   {
      using (var connection = CreateConnection())
      {
         var query = "SELECT  " +
                              "DT, " +
                              "VR, " +
                              "NVAG, " +
                              "NDOK, " +
                              "GRUZ, " +
                              "BRUTTO, " +
                              "TAR_BRS, " +
                              "TAR_DOK, " +
                              "NETTO, " +
                              "NET_DOK, " +
                              "MUSOR, " +
                              "CEX, " +
                              "TARIF, " +
                              "POTR, " +
                              "PLAT, " +
                              "SKOR, " +
                              "VESY, " +
                              "TN, " +
                              "NPP, " +
                              "V13, " +
                              "V24, " +
                              "V12, " +
                              "V34, " +
                              "PP, " +
                              "PR, " +
                              "DELTA, " +
                              "N_TEPLOVOZ, " +
                              //"VR_PRV," +
                              "POGRESHNOST, " +
                              "REJVZVESH, " +
                              "PLATFORMS_TARA, " +
                              "PLATFORMS_BRUTTO, " +
                              "Id, " +
                              "ID_PLATFORMS " +
                      "FROM gras " +
                      "where DT = @DT " +
                      "and VR = @VR " +
                      "and NVAG = @NVAG " +
                      "and VESY = @VESY";

         var parameters = new { DT = pDt, VR = pVr, NVAG = nvag, VESY = _vikno };
         return await connection.QueryFirstOrDefaultAsync<GrasModel>(query, parameters);
      }
   }
}
