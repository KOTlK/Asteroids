using System;
using Game.Runtime.GameLoop;
using UnityEngine;

namespace Game.Runtime.Input.Ship
{
    public class EnemyShipInput : IShipInput, ILoop
    {
        private readonly float _delay;

        private float _timePassed;

        public EnemyShipInput(float delay)
        {
            _delay = delay;
        }

        public bool ShootingMainGun { get; private set; }
        public Vector2 MovementDirection { get; }
        
        public void Execute(float deltaTime)
        {
            ShootingMainGun = false;

            _timePassed += deltaTime;

            if (_timePassed >= _delay)
            {
                ShootingMainGun = true;
                _timePassed = 0;
            }
        }
    }
}