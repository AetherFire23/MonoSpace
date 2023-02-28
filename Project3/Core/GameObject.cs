using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public abstract class GameObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public bool MustDestroyWhenOOB = true;
        public RectangleF Collider = RectangleF.Empty;
        public bool Enabled = true;

        //  public List<Component>
        protected SpriteBatch _spriteBatch;

        public GameObject()
        {
            _spriteBatch = Entities.SpriteBatch;
        }


        public abstract void Update();
        public abstract void Draw();

        public virtual void HandleCollision(List<GameObject> colliders)
        {

        }

        public List<GameObject> GetCollidingObjects(List<GameObject> gameObjects)
        {
            var collided = gameObjects.Where(x =>
                x.Collider.Intersects(Collider)
                && !x.Equals(this)).ToList();
            return collided;
        }
    }
}
