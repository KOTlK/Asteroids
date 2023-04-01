using UnityEngine;

namespace Game.Runtime.Physics
{
    public class CircleCollider : ICollider
    {
        private Circle _origin;

        public CircleCollider(Circle origin)
        {
            _origin = origin;
        }

        public Vector3 Position
        {
            get => _origin.Center;
            set => _origin.Center = value;
        }

        public bool Cast(ICollider collider)
        {
            return collider.Cast(_origin);
        }

        public bool Cast(AABB aabb)
        {
            return CollisionDetectionAlgorithms.AABBCircle(aabb, _origin);
        }

        public bool Cast(Circle circle)
        {
            return CollisionDetectionAlgorithms.CirclePair(circle, _origin);
        }
    }
}