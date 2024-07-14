using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Для подстановки брутто как тары на 9-х, ... ?


namespace BosVesAppLibrary.Models
{
   public class BruttoAsTaraModel
   {

      [DataType(DataType.Date)]
      public DateTime DT { get; set; } = DateTime.Now;    // дата взвешивания состава

      public TimeSpan VR { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

      [NotMapped]
      [DataType(DataType.Time)]
      public DateTime VRDateTime => DateTime.Today.Add(VR);

      [StringLength(10, ErrorMessage = "Максимальная длина 10 символов")]
      public string NVAG { get; set; }                // номер вагона

      [StringLength(20, ErrorMessage = "Максимальная длина 20 символов")]
      public string GRUZ { get; set; }                // наименование груза

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка BRUTTO")]
      public double? BRUTTO { get; set; }             // вес брутто

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка TAR_BRS")]
      public double? TAR_BRS { get; set; }            // тара фактическая

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка TAR_DOK")]
      public double? TAR_DOK { get; set; }            // тара из документа уждт

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка NETTO")]
      public double? NETTO { get; set; }              // нетто фактическое

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка NET_DOK")]
      public double? NET_DOK { get; set; }            // нетто из документа уждт

      [Range(0, 999, ErrorMessage = "Please enter a valid non-negative integer.")]
      public Int16? CEX { get; set; } = null;               // цех получатель

      [StringLength(30, ErrorMessage = "Максимальная длина 30 символов")]
      public string POTR { get; set; }                // потребитель

      [StringLength(30, ErrorMessage = "Максимальная длина 30 символов")]
      public string PLAT { get; set; }                // плательщик

      [Range(0, 99, ErrorMessage = "Диапазон ввода от 1 до 99")]
      public Int16? VESY { get; set; }                 // номер весов

      [Range(0, int.MaxValue, ErrorMessage = "Введите корректный номер")]
      public int? TN { get; set; }                     // табельный номер последнего сохранявшего изменения

      [Range(0, 100, ErrorMessage = "Диапазон ввода от 1 до 100")]
      public Int16? NPP { get; set; }                  // номер по порядку в составе

      [StringLength(30, ErrorMessage = "Максимальная длина 30 символов")]
      public string N_TEPLOVOZ { get; set; }          // номер тепловоза
                                                   
      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка POGRESHNOST")]
      public int? POGRESHNOST { get; set; }            // погрешность взвешивания

      [StringLength(11, ErrorMessage = "Максимальная длина 11 символов")]
      public string REJVZVESH { get; set; }              // режим взвешивания

      [Range(0, int.MaxValue, ErrorMessage = "Ошибка ID")]
      public int? ID { get; }                          // уникальный идентификатор записи

      [StringLength(8, ErrorMessage = "Максимальная длина 8 символов")]
      public string PLATFORMS_TARA { get; set; }      // комбинация включённых платформ при взвешивании

      [StringLength(8, ErrorMessage = "Максимальная длина 8 символов")]
      public string PLATFORMS_BRUTTO { get; set; }    // комбинация включённых платформ при взвешивании

      [Range(0, int.MaxValue, ErrorMessage = "Ошибка подстановки ID ")]
      public int? ID_PLATFORMS { get; set; }          // id записи, которая была взята для подставления тары

   }
}
