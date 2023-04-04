using System;
using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public class Asteroid : ILoop, IDisposable
    {
        private readonly IAsteroidView _view;
        private readonly IObjectDestroyer<Asteroid> _destroyer;
        private readonly ICollider _collider;
        private readonly IColliderCaster<IDamageable> _colliderCaster;
        private readonly IViewport _viewport;
        private readonly float _speed;
        private readonly float _damage;

        private Vector3 _position;

        public Asteroid(IAsteroidView view, IViewport viewport, IObjectDestroyer<Asteroid> destroyer, ICollider collider, IColliderCaster<IDamageable> colliderCaster, float speed, float damage)
        {
            _view = view;
            _viewport = viewport;
            _destroyer = destroyer;
            _collider = collider;
            _colliderCaster = colliderCaster;
            _speed = speed;
            _damage = damage;
            _position = collider.Position;
        }

        public void Execute(float deltaTime)
        {
            _position += Vector3.down * (_speed * deltaTime);

            _view.Position = _position;
            _collider.Position = _position;

            var castHit = _colliderCaster.Cast(_collider);

            if (castHit.Occure)
            {
                castHit.Target.ApplyDamage(_damage);
                _view.PlayExplosionAnimation();
                _view.DisposeOnAnimationEnd();
                _destroyer.Destroy(this);
            }

            var viewportPosition = _viewport.WorldToViewport(_position);
            if (viewportPosition.y < -1.5f)
            {
                _destroyer.Destroy(this);
            }
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}