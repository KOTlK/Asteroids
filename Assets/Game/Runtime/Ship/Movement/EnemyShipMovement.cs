using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Ship.Movement
{
    public class EnemyShipMovement : IShipMovement
    {
        private readonly IEnemyShipInput _input;
        private readonly float _speed;
        private readonly IViewport _viewport;
        private readonly IShipView _shipView;
        private readonly ICollider _shipCollider;

        public EnemyShipMovement(float speed, IEnemyShipInput input, IViewport viewport, IShipView shipView, ICollider shipCollider, Vector3 startPosition)
        {
            _input = input;
            _speed = speed;
            _viewport = viewport;
            _shipView = shipView;
            _shipCollider = shipCollider;
            Position = startPosition;
        }

        public Vector3 Position { get; set; }
        
        public void Execute(float deltaTime)
        {
            var nextPosition = Position + _input.MovementDirection * (_speed * deltaTime);

            var viewportPosition = _viewport.WorldToViewport(nextPosition);
            
            if (viewportPosition.x <= -0.1f)
            {
                nextPosition.x = _viewport.ViewportToWorld(new Vector3(1.1f, 0, 0)).x;
            }
            else if (viewportPosition.x >= 1.1f)
            {
                nextPosition.x = _viewport.ViewportToWorld(new Vector3(-0.1f, 0, 0)).x;
            }

            if (viewportPosition.y is > 1 or < 0)
            {
                _input.ReverseDirection();
            }

            Position = nextPosition;
            _shipView.Position = Position;
            _shipCollider.Position = Position;
        }

        public void Visualize(IShipInterface view)
        {
            view.DisplayPosition(Position);
        }
    }
}