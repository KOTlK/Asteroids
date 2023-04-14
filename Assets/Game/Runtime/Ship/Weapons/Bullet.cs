using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class Bullet : IBullet
    {
        private readonly IBody<IDamageable> _body;
        private readonly float _speed;
        private readonly float _damage;
        private readonly IBulletView _view;
        private readonly IObjectDestructor<IBullet> _destructor;

        private float _lifeTime;
        private Vector3 _direction = Vector3.zero;

        private const float MaxLifeTime = 15f;

        public Bullet(IBody<IDamageable> body, IBulletView view, IObjectDestructor<IBullet> destructor, float speed, float damage)
        {
            _body = body;
            _speed = speed;
            _damage = damage;
            _view = view;
            _destructor = destructor;
        }

        public void Execute(float deltaTime)
        {
            _lifeTime += deltaTime;

            if (_lifeTime >= MaxLifeTime)
            {
                _destructor.Destroy(this);
                return;
            }

            _body.Position += _direction * (_speed * deltaTime);
            _view.Position = _body.Position;

            var castHit = _body.Cast();

            if (castHit.Occure)
            {
                castHit.Target.ApplyDamage(_damage);
                _view.PlayHitAnimation();
                _destructor.Destroy(this);
            }
        }

        public void Shoot(Vector3 direction)
        {
            _direction = direction;
            _view.PlayShootSound();
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}