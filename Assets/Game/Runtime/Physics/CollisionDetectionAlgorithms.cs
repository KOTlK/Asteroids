using System;
using UnityEngine;

namespace Game.Runtime.Physics
{
    public static class CollisionDetectionAlgorithms
    {
        public static bool AABBCircle(AABB aabb, Circle circle)
        {
            var difference = circle.Center - aabb.Center;
            var halfExtents = aabb.HalfExtents;
            var clamped = Clamp(difference, -halfExtents, halfExtents);
            var closest = aabb.Center + clamped;
            difference = closest - circle.Center;

            return difference.sqrMagnitude <= circle.Radius * circle.Radius;
        }

        public static bool AABBPair(AABB first, AABB second)
        {
            return first.Center.x + first.Size.x >= second.Center.x &&
                   second.Center.x + second.Size.x >= first.Center.x &&
                   first.Center.y + first.Size.y >= second.Center.y &&
                   second.Center.y + second.Size.y >= first.Center.y;
        }

        public static bool CirclePair(Circle first, Circle second)
        {
            var dx = first.Center.x - second.Center.x;
            var dy = first.Center.y - second.Center.y;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            return distance <= first.Radius + second.Radius;
        }
        
        private static Vector3 Clamp(Vector3 vector, Vector3 min, Vector3 max)
        {
            return new Vector3(Mathf.Clamp(vector.x, min.x, max.x), Mathf.Clamp(vector.y, min.y, max.y), Mathf.Clamp(vector.z, min.z, max.z));
        }
    }
}