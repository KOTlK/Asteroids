using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game.Runtime.Physics
{
    public class CollidersWorld<T> : ICollidersWorld<T>
    {
        private readonly Dictionary<ICollider, T> _collidersMap = new();

        public T this[ICollider collider] => _collidersMap[collider];
        
        public void Add(ICollider collider, T target)
        {
            _collidersMap[collider] = target;
        }

        public void Remove(ICollider collider)
        {
            _collidersMap.Remove(collider);
        }

        public void Remove(T obj)
        {
            var collider = _collidersMap.First(pair => pair.Value.Equals(obj)).Key;
            _collidersMap.Remove(collider);
        }

        IEnumerator<ICollider> IEnumerable<ICollider>.GetEnumerator() => _collidersMap.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _collidersMap.GetEnumerator();
    }
}