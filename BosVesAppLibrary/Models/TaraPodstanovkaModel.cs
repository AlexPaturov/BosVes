// Подстановка тары под брутто для подсчёта нетто из ранее взвешенных вагонов

namespace BosVesAppLibrary.Models
{
    public class TaraPodstanovkaModel
    {
        public DateTime DT { get; set; }                // дата взвешивания состава
        public DateTime VR { get; set; }                // время взвешивания состава
        public string NVAG { get; set; }                // номер вагона
        public decimal BRUTTO { get; set; }             // вес брутто
        public decimal TAR_BRS { get; set; }            // тара фактическая
        public decimal NETTO { get; set; }              // нетто фактическое
        public Int16 VESY { get; set; }                 // номер весов
        public int ID { get; }                          // уникальный идентификатор записи
                                                        //  
   }
}
