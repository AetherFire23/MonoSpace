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
    public class Parallax : GameObject
    {
        private readonly BackImage _backImage;
        private readonly ForeImage _foreImage;

        public Parallax(BackImage backImage, ForeImage foreImage)
        {
            _backImage = backImage;
            _foreImage = foreImage;
            SetStartPosition();
        }

        private void SetStartPosition()
        {
            // donc faut ca commence avec une partie de lecran

            // width est de 900 faque 

            var startHeight = -2560 + _spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight;

            _backImage.Position = new Vector2(0, startHeight);
            _foreImage.Position = new Vector2(0, startHeight);
        }

    
        public override void Update()
        {
            _backImage.Position = _backImage.Position.Move(0, 2 * Time.Delta/200);
            _foreImage.Position = _foreImage.Position.Move(0, 1 * Time.Delta / 200);
        }

        public override void Draw()
        {

        }
    }
}
