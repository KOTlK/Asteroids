using UnityEngine;

namespace Game.Runtime.Physics
{
    public class Body<TCollideWith> : IBody<TCollideWith>
    {
        private readonly ICollider _collider;
        private readonly IColliderCaster<TCollideWith> _targets;

        public Body(ICollider collider, IColliderCaster<TCollideWith> targets, Vector3 startPosition)
        {
            _collider = collider;
            Position = startPosition;
            _targets = targets;
        }

        public Vector3 Position
        {
            get => _collider.Position;
            set => _collider.Position = value;
        }

        public Collision<TCollideWith> Cast()
        {
            return _targets.Cast(_collider);
        }
    }
}