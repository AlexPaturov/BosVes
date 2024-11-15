﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Для подстановки брутто как тары на 9-х, ... ?


namespace BosVesAppLibrary.Models
{
   public class BruttoAsTaraModel : ICloneable
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

      [Range(0, 99, ErrorMessage = "Диапазон ввода от 1 до 99")]
      public Int16? VESY { get; set; }                 // номер весов

      [Range(0, int.MaxValue, ErrorMessage = "Введите корректный номер")]
      public int? TN { get; set; }                     // табельный номер последнего сохранявшего изменения

      [Range(0, 100, ErrorMessage = "Диапазон ввода от 1 до 100")]
      public Int16? NPP { get; set; }                  // номер по порядку в составе

      [Range(0, int.MaxValue, ErrorMessage = "Ошибка Id")]
      public int? ID { get; }                          // уникальный идентификатор записи

      [StringLength(15, ErrorMessage = "Максимальная длина 15 символов")]
      public string NAIM { get; set; }                // наименование цеха

      public object Clone()
      {
         return MemberwiseClone();
      }
   }
}
