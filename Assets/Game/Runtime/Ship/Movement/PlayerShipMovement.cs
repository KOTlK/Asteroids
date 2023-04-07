using System;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Ship.Movement
{
    public class PlayerShipMovement : IShipMovement
    {
        private readonly IMovementInput _input;
        private readonly ShipStats _stats;
        private readonly IViewport _viewport;
        private readonly IShipView _shipView;
        private readonly ICollider _collider;
        
        private Vector3 _velocity;

        private const float MovementThreshold = 0.01f;

        public PlayerShipMovement(IMovementInput input, ShipStats stats, IViewport viewport, IShipView shipView, ICollider collider) 
            : this(input, stats, viewport, shipView, collider, Vector3.zero)
        {
        }

        public PlayerShipMovement(IMovementInput input, ShipStats stats, IViewport viewport, IShipView shipView, ICollider collider, Vector3 position)
        {
            _input = input;
            _stats = stats;
            _viewport = viewport;
            _shipView = shipView;
            _collider = collider;
            Position = position;
        }

        public Vector3 Position { get; set; }

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

            var nextPosition = Position + _velocity * deltaTime;

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
                nextPosition.y = Position.y;
                _velocity.y = 0;
            }


            Position = nextPosition;
            _shipView.Position = Position;
            _collider.Position = Position;
        }

        public void Visualize(IShipInterface view)
        {
            view.DisplayPosition(Position);
            view.DisplayVelocity(_velocity);
        }
    }
}