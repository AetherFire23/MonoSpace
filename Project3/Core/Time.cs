using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public static class Time
    {
        public static float Delta = 0f;
        public static void Update(GameTime time)
        {
            Delta = (float)time.ElapsedGameTime.TotalMilliseconds;

        }
    }
}
