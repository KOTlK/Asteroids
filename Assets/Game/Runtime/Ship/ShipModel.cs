using System;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public class ShipModel : ILoop, IDisposable
    {
        private readonly ShipVisualization _shipVisualization;
        private readonly ShipStats _stats;
        private readonly IShipInput _input;
        
        private Vector3 _velocity;

        private const float MovementThreshold = 0.001f;

        public ShipModel(ShipVisualization shipVisualization, IShipInput input, ShipStats stats)
        {
            _shipVisualization = shipVisualization;
            _input = input;
            _stats = stats;
        }

        public void Execute(float deltaTime)
        {
            var input = _input.MovementDirection;

            _velocity.x = Mathf.Clamp(_velocity.x + _input.MovementDirection.x * deltaTime * _stats.Acceleration, -_stats.MaxSpeed, _stats.MaxSpeed);
            _velocity.y = Mathf.Clamp(_velocity.y + _input.MovementDirection.y * deltaTime * _stats.Acceleration, -_stats.MaxSpeed, _stats.MaxSpeed);

            if (input.x == 0)
            {
                _velocity.x += -Math.Sign(_velocity.x) * _stats.Damping * deltaTime;

                if (Math.Abs(_velocity.x) <= MovementThreshold)
                    _velocity.x = 0;
            }

            if (input.y == 0)
            {
                _velocity.y += -Math.Sign(_velocity.y) * _stats.Damping * deltaTime;

                if (Math.Abs(_velocity.y) <= MovementThreshold)
                    _velocity.y = 0;
            }

            _shipVisualization.Move(_velocity * deltaTime);
            _shipVisualization.DrawUi(_velocity);
        }

        public void Dispose()
        {
            _shipVisualization.Dispose();
        }
    }
}