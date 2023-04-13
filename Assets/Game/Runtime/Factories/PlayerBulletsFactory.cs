using Game.Runtime.Factories.View;
using Game.Runtime.GameLoop.Score;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class PlayerBulletsFactory : IBulletsFactory
    {
        private readonly ICollidersWorld<IBullet> _collidersWorld;
        private readonly IColliderCaster<IDamageableTarget> _targetColliders;
        private readonly IBulletViewFactory _bulletViewFactory;
        private readonly IScore _score;
        private readonly ExecutableObjectDestructor<IBullet> _destructor = new();

        public PlayerBulletsFactory(ICollidersWorld<IBullet> collidersWorld, IColliderCaster<IDamageableTarget> targetColliders, IBulletViewFactory bulletViewFactory, IScore score)
        {
            _collidersWorld = collidersWorld;
            _targetColliders = targetColliders;
            _bulletViewFactory = bulletViewFactory;
            _score = score;
        }

        public IBullet Create(Vector3 startPosition, float damage, float speed)
        {
            var collider = new CircleCollider(new Circle()
            {
                Radius = 0.5f,
                Center = startPosition
            });

            var view = _bulletViewFactory.Create(startPosition);
            var bullet = new PlayerBullet<IDamageableTarget>(
                new Body<IDamageableTarget>(
                    collider,
                    _targetColliders,
                    startPosition),
                speed,
                damage,
                _score,
                view,
                this);
            _destructor.Add(bullet);
            _collidersWorld.Add(collider, bullet);
            return bullet;
        }

        public void Execute(float deltaTime) => _destructor.Execute(deltaTime);

        public void Destroy(IBullet obj)
        {
            _destructor.Destroy(obj);
            _collidersWorld.Remove(obj);
        }

        public void Dispose() => _destructor.Dispose();
    }
}