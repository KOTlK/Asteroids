using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class Bullet : IBullet
    {
        private readonly float _speed;
        private readonly ICollider _collider;
        private readonly IBulletView _view;
        private readonly IObjectDestructor<IBullet> _destructor;
        private readonly IKamikaze _kamikaze;

        private float _lifeTime;
        private const float MaxLifeTime = 15f;

        public Bullet(float speed, ICollider collider, IBulletView view, IObjectDestructor<IBullet> destructor, IKamikaze kamikaze)
        {
            _speed = speed;
            _collider = collider;
            _view = view;
            _destructor = destructor;
            _kamikaze = kamikaze;
            _position = collider.Position;
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

            _kamikaze.Execute(deltaTime);
            
            _position += _direction * (_speed * deltaTime);

            _collider.Position = _position;
            _view.Position = _position;

            if (_kamikaze.Destroyed)
            {
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