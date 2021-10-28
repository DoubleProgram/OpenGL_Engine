using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Engine.Math_Helper
{
   public static class Helper{
        public static Vector3 SetColorRGB(Vector3 Color) =>
            new Vector3(Color.X / 255, Color.Y / 255, Color.Z / 255);
        public static double GetRandomNumber(double minimum, double maximum){
            return new Random().NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
