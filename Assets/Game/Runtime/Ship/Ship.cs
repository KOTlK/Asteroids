using System;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public class Ship : IShip
    {
        private readonly ShipVisualization _shipVisualization;
        private readonly ICollider _collider;
        private readonly IHealth _health;
        private readonly ShipStats _stats;
        private readonly IViewport _viewport;
        private readonly IShipInput _input;
        private readonly IWeapon _weapon;
        
        private Vector3 _velocity;
        private Vector3 _position;

        private const float MovementThreshold = 0.01f;

        public Ship(ShipVisualization shipVisualization, ICollider collider, IHealth health, IShipInput input, ShipStats stats, IViewport viewport, IWeapon weapon)
        {
            _shipVisualization = shipVisualization;
            _collider = collider;
            _health = health;
            _stats = stats;
            _viewport = viewport;
            _weapon = weapon;
            _input = input;
        }

        public void Execute(float deltaTime)
        {
            _weapon.Execute(deltaTime);
            
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

            var nextPosition = _position + _velocity * deltaTime;

            var viewportPosition = _viewport.WorldToViewport(nextPosition);

            if (viewportPosition.x <= -0.1f)
            {
                nextPosition.x = _viewport.ViewportToWorld(new Vector3(1.1f, 0, 0)).x;
            }
            else if (viewportPosition.x >= 1.1f)
            {
                nextPosition.x = _viewport.ViewportToWorld(new Vector3(-0.1f, 0, 0)).x;
            }

            if (viewportPosition.y is >= 0.95f or <= 0.05f)
            {
                nextPosition.y = _position.y;
                _velocity.y = 0;
            }

            if (_input.ShootingMainGun && _weapon.CanShoot)
            {
                _weapon.Shoot();
            }


            _position = nextPosition;    
            _collider.Position = _position;
            _shipVisualization.Position = _position;
            _shipVisualization.DrawUi(_velocity, _health);
        }

        public void ApplyDamage(float amount) => _health.Lose(amount);

        public void RestoreHealth(float amount) => _health.Restore(amount);

        public void Dispose()
        {
            _shipVisualization.Dispose();
        }
    }
}