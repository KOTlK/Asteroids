using System;
using Game.Runtime.GameLoop;
using Game.Runtime.GameLoop.Score;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public class Asteroid : ILoop, IDisposable, IDamageableTarget
    {
        private readonly IAsteroidView _view;
        private readonly IObjectDestructor<Asteroid> _destructor;
        private readonly ICollider _collider;
        private readonly IViewport _viewport;
        private readonly float _speed;
        private readonly IKamikaze _kamikaze;

        private Vector3 _position;

        public Asteroid(IAsteroidView view, IViewport viewport, IObjectDestructor<Asteroid> destructor, ICollider collider, float speed, IKamikaze kamikaze)
        {
            _view = view;
            _destructor = destructor;
            _collider = collider;
            _viewport = viewport;
            _speed = speed;
            _kamikaze = kamikaze;
            _position = collider.Position;
            ScorePerKill = new System.Random().Next(1, 5);
        }

        public int ScorePerKill { get; }
        public bool IsDead { get; private set; } = false;


        public void Execute(float deltaTime)
        {
            _position += Vector3.down * (_speed * deltaTime);

            _view.Position = _position;
            _collider.Position = _position;

            _kamikaze.Execute(deltaTime);

            if (_kamikaze.Destroyed)
            {
                _view.PlayExplosionAnimation();
                _view.DisposeOnAnimationEnd();
                _destructor.Destroy(this);
            }

            var viewportPosition = _viewport.WorldToViewport(_position);
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
            _view.PlayExplosionAnimation();
            _view.DisposeOnAnimationEnd();
            _destructor.Destroy(this);
        }
    }
}