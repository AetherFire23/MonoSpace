using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public static class Entities
    {
        private static List<GameObject> _entities = new List<GameObject>();
        private static List<GameObject> _enabledEntities = new List<GameObject>();

        // since entities often want to modify the entitiesList, must create a waitlist not to temper with it while iterating
        private static List<GameObject> _waitList = new List<GameObject>();
        private static List<GameObject> _toDestroy = new List<GameObject>();
        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice _graphicsDevice;

        private static Container _container;


        // Could add an option when registering with DI where I can choose the order of execution of scripts

        public static void Initialize(SpriteBatch spriteBatch,
            GraphicsDevice device,
            Container container)
        {
            _container = container;
            SpriteBatch = spriteBatch;
            _graphicsDevice = device;
        }

        public static GameObject Instantiate(GameObject obj)
        {
            _waitList.Add(obj);

            return obj;
        }

        public static GameObject FindGameObject(GameObject obj)
        {
            return _entities.FirstOrDefault(x => x.Id == obj.Id);
        }

        public static void Destroy(GameObject obj)
        {
            obj.Enabled = false;
            _toDestroy.Add(obj);
        }

        public static void HandleCollisions()
        {
            var entitiesWithColliders = _enabledEntities.Where(g => !g.Collider.IsEmpty).ToList();

            foreach (var gameObject in entitiesWithColliders)
            {
                var collided = gameObject.GetCollidingObjects(entitiesWithColliders);

                if (!collided.Any()) continue;

                gameObject.HandleCollision(collided);
            }
        }

        public static void Update()
        {
            RefreshEnabledEntities();
            HandleCollisions();
            UpdateEntities();
            DeleteOutOfBounds();
            RemoveDestroyedAndClear();
        }

        private static void RefreshEnabledEntities()
        {
            _enabledEntities = _entities.Where(e => e.Enabled).ToList();
        }

        private static void UpdateEntities()
        {
            _enabledEntities.ForEach(e => e.Update());
            _entities.AddRange(_waitList);
            _waitList.Clear();
        }

        public static void Draw()
        {
            _enabledEntities.ForEach(gameObject => gameObject.Draw());
        }

        private static void DeleteOutOfBounds()
        {
            _entities.RemoveAll(g => g.MustDestroyWhenOOB && IsOutOfBounds(g));
        }

        private static void RemoveDestroyedAndClear()
        {
            foreach (var gameObject in _toDestroy)
            {
                _entities.Remove(gameObject);
            }

            _toDestroy.Clear();
        }

        private static bool IsOutOfBounds(GameObject gameObject)
        {
            // devrait changer pour viewport positions
            bool isOOBX = gameObject.Position.X < -1000
                || gameObject.Position.Y < -1000
                || gameObject.Position.X > _graphicsDevice.Adapter.CurrentDisplayMode.Width + 1000
                || gameObject.Position.Y > _graphicsDevice.Adapter.CurrentDisplayMode.Height + 1000;
            return isOOBX;
        }
    }
}
