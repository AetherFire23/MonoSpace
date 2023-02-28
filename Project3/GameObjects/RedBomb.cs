using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Core;
using Project3.Extensiens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project3.GameObjects
{
    public class RedBomb : GameObject
    {

        private float _width = 350;
        private float _height = 350;

        public RedBomb(Vector2 spawnPosition)
        {
            this.Position = spawnPosition;
            var adjustedx = spawnPosition.X - _width / 2;
            this.Collider = new RectangleF(adjustedx, spawnPosition.Y, _width, _height);
        }

        public override void Update()
        {
            Position = Position.Move(0, Time.Delta * -0.4f);
        }

        public override void Draw()
        {
            var adjustedx = Position.X - _width / 2;

            Vector2 spriteOrigin = new Vector2(11.5f, 0);
            Rectangle cutoff = new Rectangle(80, 0, 32, 32);



            this.Collider = new RectangleF(adjustedx, Position.Y, _width, _height);

          //  _spriteBatch.DrawRectangle(Collider, Color.Green, 1);
            _spriteBatch.Draw(Resources.Beams, this.Position, cutoff, Color.White, 0f, spriteOrigin, 10f, SpriteEffects.None, 1f);
        }
    }
}
