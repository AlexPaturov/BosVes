// Вагоны из централизованной системы весоизмерения
namespace BosVesAppLibrary.Models
{
    public class VikVagModel
    {
        public int id { get; set; }                         // Идентификатор вагона
        public string vikno { get; set; }                   // Номер весов
        public DateTime datetime { get; set; }              // Дата и время взвешивания
        public DateTime datetime_controller { get; set; }   // Дата и время взвешивания с контроллера
        public decimal brutto { get; set; }                 // Вес брутто
        public string direct { get; set; }                  // Направление движения
        public Int16 processed { get; set; }                // Признак окончания обработки
        public Int16 npp { get; set; }                      // Номер по порядку
        public string type { get; set; }                    // Тип (вагон/локомотив/авто)
        public Int16 kol_os { get; set; }                   // Кол-во осей
        public decimal speed { get; set; }                  // Скорость
        public decimal bogie1 { get; set; }                 // Вес 1-ой тележки
        public decimal bogie2 { get; set; }                 // Вес 2-ой тележки
        public decimal bogie1r1 { get; set; }               // Вес 1-ой тележки на ближней рельсе, кг
        public decimal bogie1r2 { get; set; }               // Вес 1-ой тележки на дальней рельсе, кг
        public decimal bogie2r1 { get; set; }               // Вес 2-ой тележки на ближней рельсе, кг
        public decimal bogie2r2 { get; set; }               // Вес 2-ой тележки на дальней рельсе, кг
        public decimal shift_pop { get; set; }              // Поперечное смещение, мм
        public decimal shift_pro { get; set; }              // Продольное смещение, мм
        public string nvag_z { get; set; }                  // Номер вагона из заявки
        public string nvag_v { get; set; }                  // Номер вагона из видеосистемы
        public string nvag_e { get; set; }                  // Номер вагона, введённый весовщиком
        public int video_id { get; set; }                   // Идентификатор распознанного номера вагона из видеосистемы 
        public string stat1 { get; set; }                   // Пользователь, выполнивший корректировку
        public DateTime stat2 { get; set; }                 // Дата корректировки
        public string kalibrovka { get; set; }              // Калибровочная контрольная сумма
        public string kalibrovka2 { get; set; }             // Калибровочная контрольная сумма (нуль весов)
        public int train_id { get; set; }                   // Идентификатор состава
        public DateTime datper { get; set; }                // Дата перекачки в учётную БД
        public string wtype { get; set; }                   // Тип взвешивания (тарировка, брутто)
        public DateTime datew1 { get; set; }                // Дата и время 1-го запроса веса (для статики)
        public string prim { get; set; }                    // Примечания
        public int securos_vag_id { get; set; }             // 
        public int securos_vag_nvag { get; set; }           // 
    }
}
