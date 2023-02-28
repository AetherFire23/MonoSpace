using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project3.Core
{
    public class IMGUI : GameObject
    {
        private List<string> _messages = new List<string>();
        private List<Tuple<string, Vector2>> _directDraw = new();

        public override void Update()
        {

        }

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }

        public void AddDirect(string message, Vector2 screenPosition)
        {
            _directDraw.Add(Tuple.Create<string, Vector2>(message, screenPosition));
        }

        public override void Draw()
        {
            for (int i = 0; i < _messages.Count; i++)
            {
                var pos = new Vector2(50, 50 * i);
                var toScreen = Camera.ScreenToWorld(pos);
                _spriteBatch.DrawString(Resources.Font, _messages[i], toScreen, Color.Blue);
            }

            foreach (var kvp in _directDraw)
            {
                var toWorld = Camera.ScreenToWorld(kvp.Item2);
                _spriteBatch.DrawString(Resources.Font, kvp.Item1, toWorld, Color.Blue);
                //_spriteBatch.DrawString(Resources.Font, kvp.Item1, kvp.Item2, Color.Blue);
            }

            _messages.Clear();
            _directDraw.Clear();
        }
    }
}
