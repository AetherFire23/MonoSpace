using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Extensiens
{
    public static class Rand
    {
        public static Random r = new();

        public static double GetRandomDouble(double min, double max)
        {
            bool isRolled = false;

            double result = 0;
            while (!isRolled)
            {
                result = r.NextDouble();
                if (result >= min && result <= max)
                {
                    isRolled = true;
                }
            }
            return result;
        }
    }
}




