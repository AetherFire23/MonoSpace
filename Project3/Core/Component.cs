using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public class Component
    {
        public GameObject GameObject { get; set; }
        SpriteBatch SpriteBatch = Entities.SpriteBatch;

        public virtual void Initialize()
        {
        }

        // Update method that can be overriden by derived classes
        public virtual void Update()
        {
        }

        // Draw method that can be overriden by derived classes
        public virtual void Draw()
        {
        }
    }
}
