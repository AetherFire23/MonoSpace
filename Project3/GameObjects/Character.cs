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
    [Manager]
    public class Character : GameObject
    {
        private Size2 _size = new Size2(25, 25);

        private float _speed => Time.Delta * 0.55f;
        public float TimeSinceLastShooting = 0f;
        public float TimeSinceLastRedBomb = 500f;

        private readonly AmmoManager _ammoManager;

        public Character(AmmoManager ammoManager)
        {
            var viewPortStart = new Vector2(0.5f, 0.8f);
            var start = Camera.ViewPortToWorld(viewPortStart);
            Position = start;
            MustDestroyWhenOOB = false;
            _ammoManager = ammoManager;
        }

        public override void Update()
        {
            TimeSinceLastShooting += Time.Delta;
            TimeSinceLastRedBomb += Time.Delta;

            if (!Input.Any) return;
            RedBomb();
            ApplyMovement();
            ApplyShooting();
        }

        public override void Draw()
        {

            Vector2 spritePosition = new Vector2(0, 0);

            Vector2 spriteOrigin = new Vector2(0, 0);
            _spriteBatch.Draw(Resources.Ship, this.Position, null, Color.White, 0f, spriteOrigin, 1f, SpriteEffects.None, 1f);

            Collider = new RectangleF(Position, _size);
            //_spriteBatch.DrawRectangle(Collider, Color.Blue, 1);
        }

        private void ApplyMovement()
        {
            if (Input.AKeyPress)
            {
                Position = Position.Move(-_speed, 0);
            }

            if (Input.DKeyPress)
            {
                Position = Position.Move(_speed, 0);
            }
        }

        private void ApplyShooting()
        {
            if (!Input.WKeyPress) return;
            if (!CanShoot()) return;

            CreateLaser();
            ResetShootingTimer();
        }

        private void CreateLaser()
        {
            Entities.Instantiate(new Laser()
            {
                Position = Position,
            });
            _ammoManager.SpendAmmo();
        }

        private void RedBomb()
        {
            if (!Input.SpacePress) return;
            if (TimeSinceLastRedBomb < 500f) return;
            if (!_ammoManager.BombLoaded) return;

            CreateRedBomb();
        }

        private void CreateRedBomb()
        {
            var bomb = new RedBomb(Position);

            Entities.Instantiate(bomb);
            TimeSinceLastRedBomb = 0;
            _ammoManager.SpendBomb();
        }

        private void ResetShootingTimer()
        {
            TimeSinceLastShooting = 0;
        }

        private bool CanShoot()
        {
            bool time = TimeSinceLastShooting > 200;
            bool canShoot = time && _ammoManager.AmmoInPercent != 0;
            return canShoot;
        }
    }
}
