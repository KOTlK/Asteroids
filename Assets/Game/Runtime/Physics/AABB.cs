using UnityEngine;

namespace Game.Runtime.Physics
{
    public struct AABB
    {
        public Vector3 Center;
        public Vector3 Size;
        public Vector3 HalfExtents => Size / 2;
    }
}