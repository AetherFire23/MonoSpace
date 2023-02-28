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
    public class BackImage : GameObject
    {
        private Vector2 StartPosition;
        public BackImage()
        {
            this.MustDestroyWhenOOB = false;
            StartPosition = new Vector2(0 ,-2560 + _spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight);

        }
        public override void Update()
        {

            if (Position.Y>= 900)
            {
                Position = new Vector2(0, -2560);
            }
            //if (IsBackGroundFinished())
            //{
            //    this.Position = StartPosition;
            //}
        }

        private bool IsBackGroundFinished()
        {
            var maxVisibleHeight = _spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight;
            var screenMax = Camera.ScreenToWorld(new Vector2(0, 0));
            float arbiMax = -1100;

         

            //_gui.AddMessage($"pos{this.Position}, max : {arbiMax}");
            bool isOver = this.Position.Y > arbiMax;



            return isOver;
        }


        public override void Draw()
        {
            Vector2 spriteOrigin = new Vector2(0, 0);
            _spriteBatch.Draw(Resources.StarsBackGround, this.Position, null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 1f);
        }
    }
}
