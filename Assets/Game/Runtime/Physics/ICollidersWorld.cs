using System.Collections.Generic;

namespace Game.Runtime.Physics
{
    public interface ICollidersWorld<TTarget> : IEnumerable<ICollider>
    {
        TTarget this[ICollider collider] { get; }
        void Add(ICollider collider, TTarget target);
        void Remove(ICollider collider);
        void Remove(TTarget obj);
    }
}