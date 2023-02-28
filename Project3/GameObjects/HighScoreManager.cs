using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Core;
using Project3.Extensiens;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project3.GameObjects
{
    [Manager]
    public class HighScoreManager : GameObject
    {
        public int CurrentScore = 0;

        private readonly IMGUI _gui;

        public HighScoreManager(IMGUI gui)
        {
            _gui = gui;
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            _gui.AddDirect($"Score ! {this.CurrentScore}", Camera.ViewPortToScreen(0.2f, 0.2f));
        }

        public void IncreaseHighScore(int amount)
        {
            CurrentScore += amount;
        }
    }
}