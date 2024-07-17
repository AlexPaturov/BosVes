namespace BosVesAppLibrary.Helpers;
public static class GridCaptionHelper
{
   public static Dictionary<string, string> GpriGridChapters = new Dictionary<string, string> 
   {
      {"DT", "Дата взв."},
      {"VR", "Время взв."},
      {"NVAG", "Ном. вагона"},
      {"NDOK", "Ном. докум."},
      {"GRUZ", "Груз"},
      {"BRUTTO", "Брутто"},
      {"TAR_BRS", "Тара брс."},
      {"TAR_DOK", "Тара док."},
      {"NETTO", "Нетто"},
      {"NET_DOK", "Нетто док."},
      {"MUSOR", "Мусор %"},
      {"CEX", "Цех"},
      {"TARIF", "Тариф"},
      {"POTR", "Потребитель"},
      {"PLAT", "Плательщик"},
      {"SKOR", "Скорость"},
      {"VESY", "Весы"},
      {"TN", "Таб. ном."},
      {"NPP", "НПП"},
      {"V13", "V13"},
      {"V24", "V24"},
      {"V12", "V12"},
      {"V34", "V34"},
      {"PP", "PP"},
      {"PR", "PR"},
      {"DELTA", "DELTA"},
      {"N_TEPLOVOZ", "Ном. теплов."},
      {"POGRESHNOST", "Погрешность"},
      {"REJVZVESH", "Реж. взвеш."},
      {"PLATFORMS_TARA", "Платф. тара"},
      {"PLATFORMS_BRUTTO", "Платф. брутто"},
      {"ID_PLATFORMS", "Платф. подст."},
      {"SHABLON", "Шаблон"}
   };

   // скопированы из прихода
   public static Dictionary<string, string> GrasGridChapters = new Dictionary<string, string>
   {
      {"DT", "Дата взв."},
      {"VR", "Время взв."},
      {"NVAG", "Ном. вагона"},
      {"NDOK", "Ном. докум."},
      {"GRUZ", "Груз"},
      {"BRUTTO", "Брутто"},
      {"TAR_BRS", "Тара брс."},
      {"TAR_DOK", "Тара док."},
      {"NETTO", "Нетто"},
      {"NET_DOK", "Нетто док."},
      {"MUSOR", "Мусор %"},
      {"CEX", "Цех"},
      {"TARIF", "Тариф"},
      {"POTR", "Потребитель"},
      {"PLAT", "Плательщик"},
      {"SKOR", "Скорость"},
      {"VESY", "Весы"},
      {"TN", "Таб. ном."},
      {"NPP", "НПП"},
      {"V13", "V13"},
      {"V24", "V24"},
      {"V12", "V12"},
      {"V34", "V34"},
      {"PP", "PP"},
      {"PR", "PR"},
      {"DELTA", "DELTA"},
      {"KOD_SAP", "Код сап"},
      {"N_TEPLOVOZ", "Ном. теплов."},
      {"POGRESHNOST", "Погрешность"},
      {"REJVZVESH", "Реж. взвеш."},
      {"PLATFORMS_TARA", "Платф. тара"},
      {"PLATFORMS_BRUTTO", "Платф. брутто"},
      {"ID_PLATFORMS", "Платф. подст."},
      {"SHABLON", "Шаблон"}

   };

   // Для формы подстановки брутто как тары.
   public static Dictionary<string, string> BruttoAsTaraChapters = new Dictionary<string, string>
   {
      {"DT", "Дата взв."},       //
      {"VR", "Время взв."},      //
      {"NVAG", "Ном. вагона"},   //
      {"GRUZ", "Груз"},          //
      {"BRUTTO", "Брутто"},      //
      {"TAR_BRS", "Тара брс."},  // 
      {"TAR_DOK", "Тара док."},  //
      {"NETTO", "Нетто"},        //
      {"NET_DOK", "Нетто док."}, //
      {"CEX", "Цех"},            // Поменять? Одинаковые с NAIM
      {"POTR", "Потребитель"},   //
      {"VESY", "Весы"},          //
      {"TN", "Таб. ном."},       //
      {"NPP", "НПП"},            //
      {"ID", "id"},              //
      {"NAIM", "Имя цеха"}       // Поменять? Одинаковые с CEX
   };
















}
