// Ж.Д. расход
namespace BosVesAppLibrary.Models
{
    public class GrasModel : ICloneable
    {
        public DateTime DT { get; set; }                // дата взвешивания состава
        public DateTime VR { get; set; }                // время взвешивания состава
        public string NVAG { get; set; }                // номер вагона
        public decimal NDOK { get; set; }               // номер документа уждт
        public string GRUZ { get; set; }                // наименование груза
        public decimal BRUTTO { get; set; }             // вес брутто
        public decimal TAR_BRS { get; set; }            // тара фактическая
        public decimal TAR_DOK { get; set; }            // тара из документа уждт
        public decimal NETTO { get; set; }              // нетто фактическое
        public decimal NET_DOK { get; set; }            // нетто из документа уждт
        public decimal MUSOR { get; set; }              // процент замусоренности
        public Int16 CEX { get; set; }                  // цех получатель
        public decimal TARIF { get; set; }              // тариф из документа уждт
        public string POTR { get; set; }                // потребитель
        public string PLAT { get; set; }                // плательщик
        public decimal SKOR { get; set; }               // скорость
        public Int16 VESY { get; set; }                 // номер весов
        public int TN { get; set; }                     // табельный номер последнего сохранявшего изменения
        public Int16 NPP { get; set; }                  // номер по порядку в составе
        public decimal V13 { get; set; }
        public decimal V24 { get; set; }
        public decimal V12 { get; set; }
        public decimal V34 { get; set; }
        public decimal PP { get; set; }                 // поперечное смещение
        public decimal PR { get; set; }                 // продольное смещение
        public decimal DELTA { get; set; }              // дельта
        public DateTime DT1 { get; set; }               // дата сервера
        public DateTime VR1 { get; set; }               // время сервера
        public string KOD_SAP { get; set; }
        public string N_TEPLOVOZ { get; set; }          // номер тепловоза
        public DateTime VR_PRV { get; set; }
        public int POGRESHNOST { get; set; }            // погрешность взвешивания
        public int REJVZVESH { get; set; }              // режим взвешивания
        public string PLATFORMS_TARA { get; set; }      // комбинация включённых платформ при взвешивании
        public string PLATFORMS_BRUTTO { get; set; }    // комбинация включённых платформ при взвешивании
        public int ID { get; }                          // уникальный идентификатор записи
        public int ID_PLATFORMS { get; set; }           // id записи, которая была взята для подставления тары

      public object Clone()
      {
         return MemberwiseClone();
      }
   }
}
