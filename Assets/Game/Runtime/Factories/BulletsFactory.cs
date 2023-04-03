using System.Collections.Generic;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class BulletsFactory : MonoBehaviour, IBulletsFactory
    {
        [SerializeField] private BulletView _viewPrefab;
        [SerializeField] private float _bulletsDamage;
        [SerializeField] private float _speed;

        private readonly List<IBullet> _bullets = new();
        private readonly Queue<IBullet> _removeQueue = new();

        public IBullet Create(Vector3 startPosition, IColliderCaster<ShipModel> colliderCaster)
        {
            var collider = new CircleCollider(new Circle()
            {
                Radius = 0.5f,
                Center = startPosition
            });

            var view = Instantiate(_viewPrefab, startPosition, Quaternion.identity);
            var bullet = new Bullet(collider, colliderCaster, view, this, _bulletsDamage, _speed, startPosition);
            _bullets.Add(bullet);
            return bullet;
        }

        public void Destroy(IBullet obj)
        {
            _removeQueue.Enqueue(obj);
        }

        public void Execute(float deltaTime)
        {
            while (_removeQueue.Count > 0)
            {
                var bullet = _removeQueue.Dequeue();
                _bullets.Remove(bullet);
            }
            
            foreach (var bullet in _bullets)
            {
                bullet.Execute(deltaTime);
            }
        }
    }
}