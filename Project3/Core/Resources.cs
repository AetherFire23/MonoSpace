using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public static class Resources
    {
        public static SpriteFont Font;
        public static Texture2D StarsForeGround;
        public static Texture2D StarsBackGround;
        public static Texture2D Ship;
        public static Texture2D Beams;
        public static Texture2D EnemyShip;

        public static void LoadResources(ContentManager content)
        {
            Font = content.Load<SpriteFont>("myfont");
            StarsForeGround = content.Load<Texture2D>("spr_stars01");
            StarsBackGround = content.Load<Texture2D>("spr_stars02");
            Ship = content.Load<Texture2D>("Ship_5");
            Beams = content.Load<Texture2D>("beams");
            EnemyShip = content.Load<Texture2D>("Ship_1");
        }
    }
}
