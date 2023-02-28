using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Extensiens
{
    public static class Extens
    {
        //public static Vector2 SetX(this Vector2 self, float value)
        //{
        //    self.X = value;
        //}

        //public static Vector2 SetY(this Vector2 self, float value)
        //{
        //    self.Y = value;

        //}

        //public static Vector2 Set(this Vector2 self, float xvalue, float yvalue)
        //{
        //}

        public static Vector2 Move(this Vector2 self, float xvalue, float yvalue)
        {
            return new Vector2(self.X + xvalue, self.Y + yvalue);
        }

        public static bool IsEmptyCollider(this RectangleF self)
        {
            return self.Equals(RectangleF.Empty);
        }
    }
}
