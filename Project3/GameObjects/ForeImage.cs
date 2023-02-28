using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Core;
using Project3.Extensiens;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project3.GameObjects
{
    public class ForeImage : GameObject
    {
        private Vector2 StartPosition;
        private readonly IMGUI _gui;
        public ForeImage(IMGUI gui)
        {
            _gui = gui;
            this.MustDestroyWhenOOB = false;
            StartPosition = new Vector2(0, -2560 + _spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight);
        }

        public override void Update()
        {
            if (Position.Y >= 900)
            {
                Position = new Vector2(0, -2560);
            }
        }

        public override void Draw()
        {
            Vector2 spriteOrigin = new Vector2(0, 0);

            _spriteBatch.Draw(Resources.StarsForeGround, this.Position, null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 1f);
        }
    }
}
