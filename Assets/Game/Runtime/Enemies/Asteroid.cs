using System;
using Game.Runtime.GameLoop;
using Game.Runtime.GameLoop.Score;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public class Asteroid : ILoop, IDisposable, IDamageable
    {
        private readonly IAsteroidView _view;
        private readonly IBody<IDamageable> _body;
        private readonly IObjectDestructor<Asteroid> _destructor;
        private readonly IViewport _viewport;
        private readonly IScore _score;
        private readonly float _speed;
        private readonly float _damage;

        private int _scorePerKill;
        
        public Asteroid(IAsteroidView view,
            IBody<IDamageable> body,
            IObjectDestructor<Asteroid> destructor,
            IViewport viewport,
            IScore score,
            float speed,
            float damage)
        {
            _view = view;
            _body = body;
            _destructor = destructor;
            _viewport = viewport;
            _score = score;
            _speed = speed;
            _damage = damage;
            _scorePerKill = new System.Random().Next(1, 5);
        }

        public bool IsDead { get; private set; } = false;
        
        public void Execute(float deltaTime)
        {
            _body.Position += Vector3.down * (_speed * deltaTime);
            _view.Position = _body.Position;

            var castHit = _body.Cast();

            if (castHit.Occure)
            {
                castHit.Target.ApplyDamage(_damage);
                _view.PlayExplosionAnimation();
                _destructor.Destroy(this);
            }

            var viewportPosition = _viewport.WorldToViewport(_body.Position);
            if (viewportPosition.y < -1.5f)
            {
                _destructor.Destroy(this);
            }
        }

        public void Dispose()
        {
            _view.Dispose();
        }


        public void ApplyDamage(float amount)
        {
            IsDead = true;
            _score.Add(_scorePerKill);
            _view.PlayExplosionAnimation();
            _destructor.Destroy(this);
        }
    }
}