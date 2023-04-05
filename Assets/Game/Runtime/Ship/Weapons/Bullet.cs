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
        private readonly IColliderCaster<IDamageable> _colliderCaster;
        private readonly IBulletView _view;
        private readonly IObjectDestructor<IBullet> _destructor;

        private float _lifeTime;
        private const float MaxLifeTime = 15f;

        public Bullet(ICollider collider, IColliderCaster<IDamageable> colliderCaster, IBulletView view, IObjectDestructor<IBullet> destructor, float damage, float speed, Vector3 startPosition)
        {
            _damage = damage;
            _speed = speed;
            _collider = collider;
            _colliderCaster = colliderCaster;
            _view = view;
            _destructor = destructor;
            _position = startPosition;
        }

        private Vector3 _position;
        private Vector3 _direction = Vector3.zero;

        public void Execute(float deltaTime)
        {
            _lifeTime += deltaTime;

            if (_lifeTime >= MaxLifeTime)
            {
                _destructor.Destroy(this);
                return;
            }
            
            _position += _direction * (_speed * deltaTime);

            _collider.Position = _position;
            _view.Position = _position;

            var castResult = _colliderCaster.Cast(_collider);

            if (castResult.Occure)
            {
                castResult.Target.ApplyDamage(_damage);
                _view.PlayHitAnimation();
                _view.DisposeOnAnimationEnd();
                _destructor.Destroy(this);
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