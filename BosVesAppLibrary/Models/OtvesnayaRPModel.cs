// Отвесная общая для ж.д прихода и расхода
namespace BosVesAppLibrary.Models
{
    public class OtvesnayaRPModel
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
        public Int16 VESY { get; set; }                 // номер весов
        public int TN { get; set; }                     // табельный номер последнего сохранявшего изменения
        public Int16 NPP { get; set; }                  // номер по порядку в составе
        public string N_TEPLOVOZ { get; set; }          // номер тепловоза
        public int POGRESHNOST { get; set; }            // погрешность взвешивания
        public int REJVZVESH { get; set; }              // режим взвешивания
        public int ID { get; }                          // уникальный идентификатор записи

    }
}
