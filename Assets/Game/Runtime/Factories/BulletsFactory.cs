using Game.Runtime.Factories.View;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class BulletsFactory : IBulletsFactory
    {
        private readonly ICollidersWorld<IBullet> _collidersWorld;
        private readonly IColliderCaster<IDamageable> _targetColliders;
        private readonly IBulletViewFactory _bulletViewFactory;
        private readonly ExecutableObjectDestructor<IBullet> _destructor = new();

        public BulletsFactory(ICollidersWorld<IBullet> collidersWorld, IColliderCaster<IDamageable> targetColliders, IBulletViewFactory bulletViewFactory)
        {
            _collidersWorld = collidersWorld;
            _targetColliders = targetColliders;
            _bulletViewFactory = bulletViewFactory;
        }

        public IBullet Create(Vector3 startPosition, float damage, float speed)
        {
            var collider = new CircleCollider(new Circle()
            {
                Radius = 0.5f,
                Center = startPosition
            });

            var view = _bulletViewFactory.Create(startPosition);
            var bullet = new Bullet(collider, _targetColliders, view, this, damage, speed, startPosition);
            _destructor.Add(bullet);
            _collidersWorld.Add(collider, bullet);
            return bullet;
        }

        public void Destroy(IBullet obj)
        {
            _collidersWorld.Remove(obj);
            _destructor.Destroy(obj);
        }

        public void Execute(float deltaTime) => _destructor.Execute(deltaTime);
    }
}