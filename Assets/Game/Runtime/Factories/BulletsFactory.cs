using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    //Didn't wanna make view factories, so it's MonoBehaviour
    public class BulletsFactory : MonoBehaviour, IBulletsFactory
    {
        [SerializeField] private BulletView _viewPrefab;

        private readonly ExecutableObjectDestroyer<IBullet> _destroyer = new();

        private ICollidersWorld<IBullet> _collidersWorld;
        private IColliderCaster<IDamageable> _targetColliders;

        public void Init(ICollidersWorld<IBullet> collidersWorld, IColliderCaster<IDamageable> targetColliders)
        {
            _collidersWorld = collidersWorld;
            _targetColliders = targetColliders;
        }

        public IBullet Create(Vector3 startPosition,  float damage, float speed)
        {
            var collider = new CircleCollider(new Circle()
            {
                Radius = 0.5f,
                Center = startPosition
            });

            var view = Instantiate(_viewPrefab, startPosition, Quaternion.identity);
            var bullet = new Bullet(collider, _targetColliders, view, this, damage, speed, startPosition);
            _destroyer.Add(bullet);
            _collidersWorld.Add(collider, bullet);
            return bullet;
        }

        public void Destroy(IBullet obj)
        {
            _collidersWorld.Remove(obj);
            _destroyer.Destroy(obj);
        }

        public void Execute(float deltaTime) => _destroyer.Execute(deltaTime);
    }
}