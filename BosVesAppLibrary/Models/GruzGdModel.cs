// Справочник Ж.Д. грузов
namespace BosVesAppLibrary.Models
{
    public class GruzGdModel : ICloneable
    {
        public string GRUZIK { get; set; }
        public string KOD_SAP { get; set; }
        public string NAME_SAP { get; set; }
        public string NAME_OZM { get; set; }

      public object Clone()
      {
         return MemberwiseClone();
      }
   }
}
