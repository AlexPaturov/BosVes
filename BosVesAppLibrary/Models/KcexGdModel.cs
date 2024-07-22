// Справочник цехов
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BosVesAppLibrary.Models
{
    public class KcexGdModel : ICloneable
    {

      [Range(0, 999, ErrorMessage = "Please enter a valid non-negative integer.")]
      public Int16? CEX { get; set; } = null;

      [Column(TypeName = "VARCHAR")]
      [StringLength(15)]
      public string NAIM { get; set; }

      public object Clone()
      {
         return MemberwiseClone();
      }
   }
}
