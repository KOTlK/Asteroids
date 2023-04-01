using UnityEngine;

namespace Game.Runtime.Physics
{
    public interface ICollider
    {
        Vector3 Position { get; set; }
        bool Cast(ICollider collider);
        bool Cast(AABB aabb);
        bool Cast(Circle circle);
    }
}