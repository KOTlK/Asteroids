using Game.Runtime.GameLoop;
using Game.Runtime.GameLoop.Score;
using Game.Runtime.Physics;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class PlayerBullet<TTargets> : IBullet
    where TTargets : IDamageable, ITarget
    {
        private readonly IBody<TTargets> _body;
        private readonly float _speed;
        private readonly float _damage;
        private readonly IScore _score;
        private readonly IBulletView _view;
        private readonly IObjectDestructor<IBullet> _objectDestructor;

        private Vector3 _velocity;
        private float _lifeTime;

        private const float TimeToLive = 10f;

        public PlayerBullet(IBody<TTargets> body, float speed, float damage, IScore score, IBulletView view, IObjectDestructor<IBullet> objectDestructor)
        {
            _body = body;
            _speed = speed;
            _damage = damage;
            _score = score;
            _view = view;
            _objectDestructor = objectDestructor;
        }

        public void Shoot(Vector3 direction)
        {
            _velocity = direction;
        }

        public void Execute(float deltaTime)
        {
            _lifeTime += deltaTime;

            if (_lifeTime >= TimeToLive)
            {
                _objectDestructor.Destroy(this);
                return;
            }
            
            _body.Position += _velocity * (_speed * deltaTime);
            _view.Position = _body.Position;

            var collision = _body.Cast();

            if (collision.Occure)
            {
                collision.Target.ApplyDamage(_damage);
                if (collision.Target.IsDead)
                {
                    _score.Add(collision.Target.ScorePerKill);
                }
                _view.PlayHitAnimation();
                _view.DisposeOnAnimationEnd();
                _objectDestructor.Destroy(this);
            }
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}