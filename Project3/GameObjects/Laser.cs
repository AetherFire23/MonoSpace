using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Project3.Extensiens;
using Project3.Core;

namespace Project3.GameObjects
{
    public class Laser : GameObject
    {
        public Laser()
        {

        }

        public override void Update()
        {
            Position = Position.Move(0, Time.Delta * -0.4f);
        }

        public override void Draw()
        {
            this.Collider = new RectangleF(this.Position.X, Position.Y, 15,15);
            Vector2 spriteOrigin = new Vector2(0, 0);
            Rectangle cutoff = new Rectangle(0, 0, 32, 32);

         
            _spriteBatch.Draw(Resources.Beams, this.Position, cutoff, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 1f);

            //Size2 size = new Size2(5, 25);
            //Collider = new RectangleF(Position, size);
            //_spriteBatch.DrawRectangle(Collider, Color.Blue, 1);
        }
    }
}
