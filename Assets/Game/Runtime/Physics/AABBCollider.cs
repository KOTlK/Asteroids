using UnityEngine;

namespace Game.Runtime.Physics
{
    public class AABBCollider : ICollider
    {
        private AABB _shape;

        public AABBCollider(AABB shape)
        {
            _shape = shape;
        }

        public Vector3 Position
        {
            get => _shape.Center;
            set => _shape.Center = value;
        }

        public bool Cast(ICollider collider)
        {
            return collider.Cast(_shape);
        }

        public bool Cast(AABB aabb)
        {
            return CollisionDetectionAlgorithms.AABBPair(aabb, _shape);
        }

        public bool Cast(Circle circle)
        {
            return CollisionDetectionAlgorithms.AABBCircle(_shape, circle);
        }
    }
}