using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class BulletsFactory : MonoBehaviour
    {
        [SerializeField] private BulletView _viewPrefab;
        [SerializeField] private float _bulletsDamage;
        [SerializeField] private float _speed;
        
        public IBullet Create(Vector3 startPosition, IColliderCaster<ShipModel> colliderCaster)
        {
            var collider = new CircleCollider(new Circle()
            {
                Radius = 0.5f,
                Center = startPosition
            });

            var view = Instantiate(_viewPrefab, startPosition, Quaternion.identity);

            return new Bullet(_bulletsDamage, _speed, startPosition, collider, colliderCaster, view);
        }
    }
}