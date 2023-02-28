using Project3.Attributes;
using Project3.Extensiens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public class CameraController : GameObject
    {
        private float _move => 10000f * Time.Delta;
        private float _move2 => 1f * Time.Delta;
        public override void Update()
        {
            if (Input.XKeyPress)
            {
                Camera.Position = Camera.Position.Move(_move2, 0f);
            }

            if (Input.ZKeyPress)
            {
                Camera.Position = Camera.Position.Move(-_move2, 0);
            }

            //if (Input.WKeyPress)
            //{
            //    Camera.Position = Camera.Position.Move(0, -_move2);
            //}

            //if (Input.SKeyPress)
            //{
            //    Camera.Position = Camera.Position.Move(0, _move2);
            //}
        }

        public override void Draw()
        {
        }
    }
}
