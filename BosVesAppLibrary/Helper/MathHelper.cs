using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BosVesAppLibrary.Helper;
public static class MathHelper
{
   public static float GetDiff(float x, float y) 
   {
      return (float)x - y;
   }

   public static float GetSumm(float x, float y) 
   {
      return x + y;
   }
}
