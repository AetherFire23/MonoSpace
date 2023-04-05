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
    public class MotherShip : GameObject
    {
        private int _health = 10;

        private float _threshold = Camera.ViewPortToWorld(0, 1f).Y;


        private readonly EnemiesManager _enemiesManager;
        private readonly Character _character;
        private readonly IMGUI _gui;

        public MotherShip(EnemiesManager enemiesManager, IMGUI gui, Character character)
        {
            _enemiesManager = enemiesManager;
            _gui = gui;
            _character = character;
        }

        public override void Update()
        {
            DestroyEnemiesOutsideBounds();

            if (_health == 0)
            {
                GameOver();
            }
        }

        private void DestroyEnemiesOutsideBounds()
        {
            var enemiesOnMotherShip = _enemiesManager.Enemies.Where(e => e.Position.Y > _threshold && e.Enabled);

            foreach (var enemy in enemiesOnMotherShip)
            {
                _health -= 1;

                Entities.Destroy(enemy);
            }
        }

        private void GameOver()
        {
            _enemiesManager.Enemies.ForEach(x => x.Enabled = false);
            _character.Enabled = false;
        }

        public override void Draw()
        {

            _gui.AddDirect($"MotherShip Lives : {_health}", Camera.ViewPortToScreen(0.2f, 0.6f));

            if(_health == 0)
            {
                _gui.AddDirect($"You have lost.", Camera.ViewPortToScreen(0.5f, 0.5f));
                _gui.AddDirect($"You were the only hope for humanity", Camera.ViewPortToScreen(0.5f, 0.55f));
                _gui.AddDirect($"The depths of space will from now on forever be engulfed in pain", Camera.ViewPortToScreen(0.5f, 0.6f));
            }
        }
    }
}
