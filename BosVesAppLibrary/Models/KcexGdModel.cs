// Справочник цехов
namespace BosVesAppLibrary.Models
{
    public class KcexGdModel : ICloneable
    {
        public Int16 CEX { get; set; }

        public string NAIM { get; set; }

      public object Clone()
      {
         return MemberwiseClone();
      }
   }
}
