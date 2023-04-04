using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public interface IAsteroidsFactory : IObjectDestroyer<Asteroid>, ILoop
    {
        Asteroid Create(IColliderCaster<IDamageable> colliderCaster, float speed, float damage, Vector3 startPosition);
    }
}