using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class BulletsFactory : MonoBehaviour, IBulletsFactory
    {
        [SerializeField] private BulletView _viewPrefab;

        private readonly ExecutableObjectDestroyer<IBullet> _destroyer = new();

        public IBullet Create(Vector3 startPosition, IColliderCaster<IDamageable> colliderCaster, float damage, float speed)
        {
            var collider = new CircleCollider(new Circle()
            {
                Radius = 0.5f,
                Center = startPosition
            });

            var view = Instantiate(_viewPrefab, startPosition, Quaternion.identity);
            var bullet = new Bullet(collider, colliderCaster, view, this, damage, speed, startPosition);
            _destroyer.Add(bullet);
            return bullet;
        }

        public void Destroy(IBullet obj) => _destroyer.Destroy(obj);

        public void Execute(float deltaTime) => _destroyer.Execute(deltaTime);
    }
}