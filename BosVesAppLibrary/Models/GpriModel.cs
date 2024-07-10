using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Ж.Д. приход


namespace BosVesAppLibrary.Models
{
   public class GpriModel
   {

      [DataType(DataType.Date)]
      public DateTime DT { get; set; } = DateTime.Now;    // дата взвешивания состава

      public TimeSpan VR { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

      [NotMapped]
      [DataType(DataType.Time)]
      public DateTime VRDateTime => DateTime.Today.Add(VR);

      [StringLength(10, ErrorMessage = "Максимальная длина 10 символов")]
      public string NVAG { get; set; }                // номер вагона

      [RegularExpression(@"^(0|-?\d{0,8}(\.\d{0,0})?)$", ErrorMessage = "Ошибка NDOK")]
      public double? NDOK { get; set; }               // номер документа уждт

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

      [RegularExpression(@"^(0|-?\d{0,5}(\.\d{0,2})?)$", ErrorMessage = "Ошибка MUSOR")]
      public double? MUSOR { get; set; }              // процент замусоренности

      [Required(ErrorMessage = "The integer value is required.")]
      [Range(0, 999, ErrorMessage = "Please enter a valid non-negative integer.")]
      public Int16? CEX { get; set; }                  // цех получатель

      [RegularExpression(@"^(0|-?\d{0,12}(\.\d{0,4})?)$", ErrorMessage = "Ошибка TARIF")]
      public double? TARIF { get; set; }              // тариф из документа уждт

      [StringLength(30, ErrorMessage = "Максимальная длина 30 символов")]
      public string POTR { get; set; }                // потребитель

      [StringLength(30, ErrorMessage = "Максимальная длина 30 символов")]
      public string PLAT { get; set; }                // плательщик

      [RegularExpression(@"^(0|-?\d{0,4}(\.\d{0,1})?)$", ErrorMessage = "Ошибка SKOR")]
      public double? SKOR { get; set; }               // скорость

      [Required(ErrorMessage = "Введите целое число")]
      [Range(0, 99, ErrorMessage = "Диапазон ввода от 1 до 99")]
      public Int16? VESY { get; set; }                 // номер весов

      [Range(0, int.MaxValue, ErrorMessage = "Введите корректный номер")]
      public int? TN { get; set; }                     // табельный номер последнего сохранявшего изменения

      [Required(ErrorMessage = "Введите целое число")]
      [Range(0, 100, ErrorMessage = "Диапазон ввода от 1 до 100")]
      public Int16? NPP { get; set; }                  // номер по порядку в составе

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка V13")]
      public double? V13 { get; set; }

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка V24")]
      public double? V24 { get; set; }

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка V12")]
      public double? V12 { get; set; }

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка V34")]
      public double? V34 { get; set; }

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка PP")]
      public double? PP { get; set; }                 // поперечное смещение

      [RegularExpression(@"^(0|-?\d{0,6}(\.\d{0,2})?)$", ErrorMessage = "Ошибка PR")]
      public double? PR { get; set; }                 // продольное смещение

      [RegularExpression(@"^(0|-?\d{0,7}(\.\d{0,1})?)$", ErrorMessage = "Ошибка DELTA")]
      public double? DELTA { get; set; }              // дельта

      [StringLength(20, ErrorMessage = "Максимальная длина 20 символов")]
      public string KOD_SAP { get; set; }

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

      [Column(TypeName = "VARCHAR")]
      [StringLength(3)]
      [Unicode(false)]
      public string SHABLON { get; set; }            // был ли взят шаблон 
   }
}
