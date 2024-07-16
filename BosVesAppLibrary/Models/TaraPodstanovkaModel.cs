// Подстановка тары из ранее взвешенных вагонов
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BosVesAppLibrary.Models
{
   public class TaraPodstanovkaModel : ICloneable
   {
      [DataType(DataType.Date)]
      public DateTime DT { get; set; }                // дата взвешивания состава

      public TimeSpan VR { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

      [NotMapped]
      [DataType(DataType.Time)]
      public DateTime VRDateTime => DateTime.Today.Add(VR);

      [StringLength(10, ErrorMessage = "Максимальная длина 10 символов")]
      public string NVAG { get; set; }                // номер вагона

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка BRUTTO")]
      public decimal BRUTTO { get; set; }             // вес брутто

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка TAR_BRS")]
      public decimal TAR_BRS { get; set; }            // тара фактическая

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка NETTO")]
      public decimal NETTO { get; set; }              // нетто фактическое

      [StringLength(20, ErrorMessage = "Максимальная длина 20 символов")]
      public string GRUZ { get; set; }                // наименование груза

      [Range(0, 99, ErrorMessage = "Диапазон ввода от 1 до 99")]
      public Int16 VESY { get; set; }                 // номер весов

      [Range(0, int.MaxValue, ErrorMessage = "Ошибка ID")]
      public int ID { get; }                          // уникальный идентификатор записи

      public object Clone()
      {
         return MemberwiseClone();
      }
   }
}
