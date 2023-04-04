using Game.Runtime.View.Health;
using UnityEngine;

namespace Game.Runtime.Ship.Hp
{
    public class Health : IHealth
    {
        private readonly float _max;
        
        private float _current;

        private const float Min = 0f;

        public Health(float max)
        {
            _max = max;
            _current = max;
        }

        public bool IsOver => _current <= Min;

        public void Lose(float amount)
        {
            SetCurrent(_current - amount);
        }

        public void Restore(float amount)
        {
            SetCurrent(_current + amount);
        }

        private void SetCurrent(float value)
        {
            _current = Mathf.Clamp(value, Min, _max);
        }

        public void Visualize(IHealthView view)
        {
            view.DisplayHealth(_current);
        }
    }
}