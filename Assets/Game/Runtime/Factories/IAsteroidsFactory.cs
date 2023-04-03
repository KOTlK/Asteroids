using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public interface IAsteroidsFactory : IObjectDestroyer<Asteroid>, ILoop
    {
        Asteroid Create(IColliderCaster<ShipModel> colliderCaster, float speed, float damage, Vector3 startPosition);
    }
}