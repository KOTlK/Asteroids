using UnityEngine;
using Random = System.Random;

namespace Game.Runtime.Input.Ship
{
    public class EnemyShipInput : IEnemyShipInput
    {
        private readonly float _delay;
        private readonly float _switchDirectionChance;
        private readonly Random _random = new();

        private float _timePassed;

        public EnemyShipInput(float delay, float switchDirectionChance, Vector3 startDirection)
        {
            _delay = delay;
            _switchDirectionChance = switchDirectionChance;
            _timePassed = delay;
            MovementDirection = startDirection;
        }

        public bool ShootingMainGun { get; private set; }
        public Vector3 MovementDirection { get; private set; }
        
        public void Execute(float deltaTime)
        {
            ShootingMainGun = false;

            _timePassed += deltaTime;

            if (_timePassed >= _delay)
            {
                ShootingMainGun = true;
                _timePassed = 0;

                var randomValue = (float)_random.NextDouble();
                var randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
                if (randomValue <= _switchDirectionChance)
                {
                    MovementDirection = randomDirection;
                }
            }
        }

        public void ReverseDirection()
        {
            MovementDirection = -MovementDirection;
        }
    }
}