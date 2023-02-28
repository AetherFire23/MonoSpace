using Project3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Models
{
    public class TimerAndCallback : GameObject
    {
        public float ElapsedFull = 0f;
        public float ElapsedInterval = 0f;

        private float _intervalMilliseconds;
        private Func<bool> _cancelCallback;
        private Action _callback;

        public TimerAndCallback(float intervalMilliseconds, Func<bool> cancelCallback, Action callBack)
        {
            _intervalMilliseconds = intervalMilliseconds;
            _cancelCallback = cancelCallback;
            _callback = callBack;
        }

        public TimerAndCallback(float intervalMilliseconds, Action callBack)
        {
            _intervalMilliseconds = intervalMilliseconds;
            _callback = callBack;
        }

        public override void Update()
        {
            ElapsedFull += Time.Delta;
            ElapsedInterval += Time.Delta;

            if (ElapsedInterval > _intervalMilliseconds) return;
            if (_cancelCallback is not null)
            {
                if (_cancelCallback()) return;
            }

            _callback();
            ElapsedInterval = 0f;
        }


        public override void Draw()
        {
        }
    }
}
