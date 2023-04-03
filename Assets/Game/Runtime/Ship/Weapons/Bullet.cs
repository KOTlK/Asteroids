using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class Bullet : IBullet
    {
        private readonly float _damage;
        private readonly float _speed;
        private readonly ICollider _collider;
        private readonly IColliderCaster<ShipModel> _colliderCaster;
        private readonly IBulletView _view;
        private readonly IObjectDestroyer<IBullet> _destroyer;

        public Bullet(ICollider collider, IColliderCaster<ShipModel> colliderCaster, IBulletView view, IObjectDestroyer<IBullet> destroyer, float damage, float speed, Vector3 startPosition)
        {
            _damage = damage;
            _speed = speed;
            _collider = collider;
            _colliderCaster = colliderCaster;
            _view = view;
            _destroyer = destroyer;
            _position = startPosition;
        }

        private Vector3 _position;
        private Vector3 _direction = Vector3.zero;

        public void Execute(float deltaTime)
        {
            _position += _direction * (_speed * deltaTime);

            _collider.Position = _position;
            _view.Position = _position;

            var castResult = _colliderCaster.Cast(_collider);

            
            if (castResult.Occure)
            {
                castResult.Target.ApplyDamage(_damage);
                _view.PlayHitAnimation();
                _view.DisposeOnAnimationEnd();
                _destroyer.Destroy(this);
            }
        }

        public void Shoot(Vector3 direction)
        {
            _direction = direction;
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}