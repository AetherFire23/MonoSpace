using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using Microsoft.Xna.Framework.Input;

namespace Project3.Core
{
    public static class Input
    {
        private static KeyboardState _keyboardState;
        public static bool WKeyPress => _keyboardState.IsKeyDown(Keys.W);
        public static bool AKeyPress => _keyboardState.IsKeyDown(Keys.A);
        public static bool SKeyPress => _keyboardState.IsKeyDown(Keys.S);
        public static bool DKeyPress => _keyboardState.IsKeyDown(Keys.D);


        public static bool XKeyPress => _keyboardState.IsKeyDown(Keys.X);
        public static bool ZKeyPress => _keyboardState.IsKeyDown(Keys.Z);
        public static bool SpacePress => _keyboardState.IsKeyDown(Keys.Space);
        public static bool Any => _keyboardState.GetPressedKeyCount() >= 0;

        public static void Update()
        {
            _keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }
    }
}
