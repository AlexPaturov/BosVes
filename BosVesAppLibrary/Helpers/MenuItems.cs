namespace BosVesAppLibrary.Helpers;

public static class MenuItems
{
   #region справочники для наполнения главного меню web bos ves
   private static Dictionary<string, string> railWeightSub = new Dictionary<string, string>();
   private static Dictionary<string, string> avtoWeightSub = new Dictionary<string, string>();
   private static Dictionary<string, string> agglomerateSub = new Dictionary<string, string>();
   private static Dictionary<string, string> castIronSub = new Dictionary<string, string>();
   private static Dictionary<string, string> scrapRentalSub = new Dictionary<string, string>();
   private static Dictionary<string, string> drosslSub = new Dictionary<string, string>();
   private static Dictionary<string, string> railWeightByTimeSub = new Dictionary<string, string>();
   #endregion

   #region справочники для заполнения меню весовщиков
   private static Dictionary<string, string> menuPrihod = new Dictionary<string, string>();
   private static Dictionary<string, string> menuRashod = new Dictionary<string, string>();
   private static Dictionary<string, string> menuAvto = new Dictionary<string, string>();
   private static Dictionary<string, string> menuChugun = new Dictionary<string, string>();
   private static Dictionary<string, string> menuVesySix = new Dictionary<string, string>();
   private static Dictionary<string, string> menuKorrectirovki = new Dictionary<string, string>();
   #endregion

   #region коды для перенаправления с главной страницы весовщиков на форму по работе с весом
   public const string redirectToAvto = "Avto";
   public const string redirectToPrihod = "Prihod";
   public const string redirectToRashod = "Rashod";
   public const string redirectToChugun = "Chugun";
   public const string redirectToBoss = "Boss";
   #endregion

   // 
   static MenuItems()
   {
      #region railWeightSub fulling
      railWeightSub.Add("Просмотр прихода", "/");
      railWeightSub.Add("Просмотр убытия", "/");
      railWeightSub.Add("Сводка по грузам", "/");
      railWeightSub.Add("Лом керамет", "/");
      railWeightSub.Add("Свод по вагинам", "/");
      railWeightSub.Add("Свод по клиентам", "/");
      railWeightSub.Add("Сверка по нетто", "/");
      railWeightSub.Add("Сводка по грузу", "/");
      railWeightSub.Add("Отвесная", "/");
      railWeightSub.Add("Протокол взвешивания", "/");
      railWeightSub.Add("Форма требования НД УЖДТ", "/");
      railWeightSub.Add("История вагона", "/");
      railWeightSub.Add("Грузоподъёмность вагона", "/");
      railWeightSub.Add("Учёт времени по весам 10", "/");
      railWeightSub.Add("История взвешивания вагона", "/");
      #endregion

      #region avtoWeightSub fulling
      avtoWeightSub.Add("Справка по грузу", "/");
      avtoWeightSub.Add("Справка по машине", "/");
      avtoWeightSub.Add("Справка по клиенту", "/");
      avtoWeightSub.Add("Справка по цеху", "/");
      avtoWeightSub.Add("Выгрузка в EXCEL", "/");
      avtoWeightSub.Add("По времени", "/");
      avtoWeightSub.Add("Файл для ЧАО КЕРАМЕТ", "/");
      #endregion

      #region agglomerateSub fulling
      agglomerateSub.Add("Печать отвесных весы 32", "/");
      agglomerateSub.Add("Агломерат по бригадам", "/");
      agglomerateSub.Add("Агломерат по сменам", "/");
      agglomerateSub.Add("Агломерат по пробам", "/");
      agglomerateSub.Add("Агломерат по времени", "/");
      agglomerateSub.Add("Просмотр агломерата", "/");
      agglomerateSub.Add("Печать отвесных", "/");
      agglomerateSub.Add("Диаграмма произв. агломерата", "/");
      #endregion

      #region castIronSub fulling
      castIronSub.Add("Взвешивания чугуновозов", "/");
      castIronSub.Add("Ковши по цехам", "/");
      castIronSub.Add("Состояние чугуновозов", "/");
      castIronSub.Add("Сводка ковшей по времени", "/");
      castIronSub.Add("Справка для диспетчера", "/");
      castIronSub.Add("Справка для диспетчера ДЦ", "/");
      castIronSub.Add("Диаграмма пр-ва чугуна", "/");
      castIronSub.Add("Сводка по выпускам", "/");
      castIronSub.Add("Шлаковозы", "/");
      castIronSub.Add("Грузы ДЦ на весах 16", "/");
      castIronSub.Add("Отвесная 16", "/");
      #endregion

      #region scrapRentalSub fulling
      scrapRentalSub.Add("Платформенные", "/");
      #endregion

      #region drosslSub fulling
      drosslSub.Add("Просмотр весы 6", "/");
      drosslSub.Add("Отвесная", "/");
      #endregion

      #region railWeightByTimeSub fulling
      railWeightByTimeSub.Add("Ж/Д весы по вр. состава", "/");
      #endregion

      #region menuPrihod fulling
      menuPrihod.Add("Ввод вагонов", "/Pages/WeighterScope/Prihod/VvodPrihod");
      menuPrihod.Add("Корректировка", "/Pages/WeighterScope/Prihod/KorrPrihod");
      menuPrihod.Add("Отвесная", "/Pages/WeighterScope/Otvesnaya/PrihodOtvesnaya");
      #endregion

      #region menuRashod fulling
      menuRashod.Add("Ввод вагонов", "/Pages/WeighterScope/Rashod/VvodRashod");
      menuRashod.Add("Корректировка", "/Pages/WeighterScope/Rashod/KorrRashod");
      menuRashod.Add("Отвесная", "/Pages/WeighterScope/Otvesnaya/RashodOtvesnaya");
      #endregion

      #region menuAvto fulling
      menuAvto.Add("Ввод авто", "/Pages/WeighterScope/Avto/VvodAvto");
      menuAvto.Add("Корректировка", "/Pages/WeighterScope/Avto/KorrAvto");
      menuAvto.Add("Отвесная", "/Pages/WeighterScope/Otvesnaya/AvtoOtvesnaya");
      #endregion

      #region menuChugun fulling
      menuChugun.Add("Ввод ковшей", "/Pages/WeighterScope/Chugun/VvodChugun");
      menuChugun.Add("Корректировка", "/Pages/WeighterScope/Chugun/KorrChugun");
      menuChugun.Add("Отвесная", "/Pages/WeighterScope/Otvesnaya/ChugunOtvesnaya");
      #endregion

      #region menuVesySix fulling
      menuVesySix.Add("Ввод приход", "/Pages/WeighterScope/VesySix/VvodVesySixPrixod");
      menuVesySix.Add("Ввод расход", "/Pages/WeighterScope/VesySix/VvodVesySixRashod");
      menuVesySix.Add("Корректировка приход", "/Pages/WeighterScope/VesySix/KorrVesySixPrihod");
      menuVesySix.Add("Корректировка расход", "/Pages/WeighterScope/VesySix/KorrVesySixRashod");
      menuVesySix.Add("Отвесная", "/Pages/WeighterScope/Otvesnaya/ChugunOtvesnaya");
      #endregion

      #region menuKorrectirovki fulling
      menuKorrectirovki.Add("Корректировка приход", "/");
      menuKorrectirovki.Add("Корректировка расход", "/");
      menuKorrectirovki.Add("Корректировка чугун", "/");
      menuKorrectirovki.Add("Корректировка 6 весы", "/");
      menuKorrectirovki.Add("Корректировка авто", "/");
      #endregion
   }

   public static Dictionary<string, string> GetRailWeightSub()
   {
      return railWeightSub;
   }

   public static Dictionary<string, string> GetAvtoWeightSub()
   {
      return avtoWeightSub;
   }

   public static Dictionary<string, string> GetAgglomerateSub()
   {
      return agglomerateSub;
   }

   public static Dictionary<string, string> GetCastIronSub()
   {
      return castIronSub;
   }

   public static Dictionary<string, string> GetScrapRentalSub()
   {
      return scrapRentalSub;
   }

   public static Dictionary<string, string> GetDrosslSub()
   {
      return drosslSub;
   }

   public static Dictionary<string, string> GetRailWeightByTimeSub()
   {
      return railWeightByTimeSub;
   }

   // 
   public static Dictionary<string, string> GetMenuRashodSub()
   {
      return menuRashod;
   }

   public static Dictionary<string, string> GetMenuPrihodSub()
   {
      return menuPrihod;
   }

   public static Dictionary<string, string> GetMenuAvtoSub()
   {
      return menuAvto;
   }

   public static Dictionary<string, string> GetMenuChugunSub()
   {
      return menuChugun;
   }

   public static Dictionary<string, string> GetMenuKorrectirovkiSub()
   {
      return menuKorrectirovki;
   }






}
