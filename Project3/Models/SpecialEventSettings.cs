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

namespace Project3.Models
{
    public class SpecialEventSettings
    {
        public bool IsActive = true;
        public bool IsGoingRight = false;

        public Vector2 CurrentSpawnPos;
        public Vector2 DefaultSpawn;



        public float TimeSinceLastEvent = 0f;
        public float TimeSinceLastEnemySpawn;
        public float EventDuration = 0f;
        public bool MustSpawnEnemy = false;
        public SpecialEventSettings()
        {
            DefaultSpawn = GetRandomSpawnLocation();
            CurrentSpawnPos = DefaultSpawn;
        }

        public Vector2 GetRandomSpawnLocation()
        {
            var randomx = Rand.GetRandomDouble(0.1f, 0.4f);
            var pos = Camera.ViewPortToWorld((float)randomx, 0f);
            return pos;
        }
    }
}
