using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Attributes;
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
    public class Enemy : GameObject
    {
        [Inject]
        private EnemiesManager _manager;

        [Inject]
        private IMGUI _gui;

        public Enemy()
        {

        }

        public override void Update()
        {
            Position = Position.Move(0, Time.Delta * 0.2f);
          //  _gui.AddMessage(Position.ToString());
        }

        public override void Draw()
        {
            Size2 size = new Size2(25, 25);

            Vector2 spriteOrigin = new Vector2(0, 0);

            _spriteBatch.Draw(Resources.EnemyShip, this.Position, null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 1f);

            Collider = new RectangleF(Position, size);

            //_spriteBatch.DrawRectangle(Collider, Color.Red, 1);
        }

        public override void HandleCollision(List<GameObject> colliders)
        {
            var lasers = colliders.Where(x =>
            x.GetType().Equals(typeof(Laser))
            || x.GetType().Equals(typeof(RedBomb)
            ));

            if (!lasers.Any()) return;


            foreach (var collider in lasers)
            {
                if (collider.GetType() == typeof(RedBomb)) continue;

                Entities.Destroy(collider);

            }

            _manager.HandleEnemyDeath(this);
            Entities.Destroy(this);
        }
    }
}
