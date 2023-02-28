using Microsoft.Xna.Framework;
using Project3.Core;
using Project3.Extensiens;
using Project3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project3.GameObjects
{
    [Manager]
    public class EnemiesManager : GameObject
    {
        public List<Enemy> Enemies = new List<Enemy>();

        public float TimeSinceLastEnemySpawn = 0f;
        private readonly HighScoreManager _scoreManager;
        private readonly Container _container;

        private SpecialEventSettings _specialEventSettings = new();

        public EnemiesManager(HighScoreManager scoreManager, Container container)
        {
            _container = container;
            _scoreManager = scoreManager;

        }

        public override void Update()
        {
            TimeSinceLastEnemySpawn += Time.Delta;

            bool mustSpawnEnemy = TimeSinceLastEnemySpawn > 1000f;

            SpecialEvent();

            if (!mustSpawnEnemy) return;

            SpawnEnemy();
        }

        public override void Draw()
        {

        }

        public void SpecialEvent()
        {
            _specialEventSettings.TimeSinceLastEvent += Time.Delta;

            if (!(_specialEventSettings.TimeSinceLastEvent > 10000f)) return;

            _specialEventSettings.IsActive = true;


            if (!_specialEventSettings.IsActive) return;

            _specialEventSettings.EventDuration += Time.Delta;
            _specialEventSettings.TimeSinceLastEnemySpawn += Time.Delta;

            if ((_specialEventSettings.TimeSinceLastEnemySpawn < 500)) return;

            _specialEventSettings.MustSpawnEnemy = true;


            var enemySpawnPosition = _specialEventSettings.IsGoingRight
                ? _specialEventSettings.CurrentSpawnPos.Move(16, 0)
                : _specialEventSettings.CurrentSpawnPos.Move(-16, 0);



            _specialEventSettings.CurrentSpawnPos = _specialEventSettings.CurrentSpawnPos.Move(50, 0);

            Enemy enemy = new Enemy()
            {
                Position = _specialEventSettings.CurrentSpawnPos,
            };
            _container.ResolveFromRegisteredTypes(enemy);

            Entities.Instantiate(enemy);
            Enemies.Add(enemy);

            _specialEventSettings.TimeSinceLastEnemySpawn = 0f;
            if (_specialEventSettings.EventDuration > 5000)
            {
                _specialEventSettings.IsActive = false;
                _specialEventSettings.TimeSinceLastEnemySpawn = 0f;
                _specialEventSettings.TimeSinceLastEvent = 0f;
                _specialEventSettings.EventDuration = 0;
                _specialEventSettings.CurrentSpawnPos = _specialEventSettings.GetRandomSpawnLocation();
            }
        }

        public void HandleEnemyDeath(Enemy deadEnemy)
        {
            _scoreManager.IncreaseHighScore(1);
        }

        private void SpawnEnemy()
        {
            var randomViewX = (float)Rand.GetRandomDouble(0.3d, 0.7d);
            var startPosWorld = Camera.ViewPortToWorld(randomViewX, 0f);
            Enemy gameObject = Entities.Instantiate(new Enemy()) as Enemy;
            gameObject = _container.ResolveFromRegisteredTypes(gameObject) as Enemy;
            gameObject.Position = startPosWorld;
            Enemies.Add(gameObject);
            TimeSinceLastEnemySpawn = 0;
        }
    }
}