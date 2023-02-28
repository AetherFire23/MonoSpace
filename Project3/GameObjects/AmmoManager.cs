using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Core;
using Project3.Extensiens;
using Project3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project3.GameObjects
{
    public class AmmoManager : GameObject
    {
        public int AmmoInPercent = 100;
        public bool BombLoaded = true;

        private float _normalReloadTime = 0f;
        private float _bombReloadTime = 0f;

        private readonly IMGUI _gui;


        private TimerAndCallback _reloadTimer;
        private TimerAndCallback _bombRealodTimer;

        public AmmoManager(IMGUI gui)
        {
            _gui = gui;

            _reloadTimer = new TimerAndCallback(300f, ReloadNormalAmmo);
            _bombRealodTimer = new TimerAndCallback(2500f, ManageBombReload);
            Entities.Instantiate(_reloadTimer);
            Entities.Instantiate(_bombRealodTimer);
        }

        public void SpendAmmo()
        {
            this.AmmoInPercent -= 2;
        }

        public void SpendBomb()
        {
            this.BombLoaded = false;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            var screenPos = Camera.ViewPortToScreen(new Vector2(0.2f, 0.4f));
            _gui.AddDirect($"Ammo : {this.AmmoInPercent}", screenPos);

            var screenPos2 = Camera.ViewPortToScreen(new Vector2(0.2f, 0.5f));
            _gui.AddDirect($"Bomb ready :{this.BombLoaded}", screenPos2);
        }

        private void ManageBombReload()
        {
            _normalReloadTime += Time.Delta;
            if (BombLoaded)
            {
                _bombReloadTime = 0f;
                return;
            }

            if (_bombReloadTime > 5000f)
            {
                BombLoaded = true;
                _bombReloadTime = 0;
            }
        }

        public void ReloadNormalAmmo()
        {
            _bombReloadTime += Time.Delta;
            if (AmmoInPercent == 100)
            {
                _normalReloadTime = 0f;
                return;
            }

            if (_normalReloadTime > 350f)
            {
                AmmoInPercent += 2;
                _normalReloadTime = 0;
            }
        }
    }
}
