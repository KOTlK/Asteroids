using Game.Runtime.Factories;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public class EnemyShip : IShip
    {
        private readonly IShipView _shipVisualization;
        private readonly IObjectDestroyer<EnemyShip> _destroyer;
        private readonly IBulletsFactory _bulletsFactory;
        private readonly ICollider _collider;
        private readonly IShipInput _input;
        private readonly IColliderCaster<IDamageable> _colliderCaster;
        private readonly EnemyShipStats _stats;
        private readonly IHealth _health;

        private Vector3 _position;

        public EnemyShip(Vector3 startPosition, IShipView shipVisualization, IObjectDestroyer<EnemyShip> destroyer, IBulletsFactory bulletsFactory, ICollider collider, IShipInput input, IColliderCaster<IDamageable> colliderCaster, EnemyShipStats stats)
        {
            _position = startPosition;
            _shipVisualization = shipVisualization;
            _destroyer = destroyer;
            _bulletsFactory = bulletsFactory;
            _collider = collider;
            _input = input;
            _colliderCaster = colliderCaster;
            _stats = stats;
            _health = new Health(stats.MaxHealth);
        }

        public void Execute(float deltaTime)
        {
            _position += Vector3.down * (_stats.Speed * deltaTime);

            _collider.Position = _position;
            _shipVisualization.Position = _position;

            var raycastHit = _colliderCaster.Cast(_collider);

            if (raycastHit.Occure)
            {
                raycastHit.Target.ApplyDamage(_stats.DamageOnCollision);
                _shipVisualization.PlayExplosionAnimation();
                _shipVisualization.DisposeOnAnimationEnd();
                _destroyer.Destroy(this);
                return;
            }

            if (_input.ShootingMainGun)
            {
                var bullet = _bulletsFactory.Create(_shipVisualization.Pivot, _stats.Damage, _stats.BulletsSpeed);
                bullet.Shoot(Vector3.down);
            }
        }

        public void Dispose()
        {
            _shipVisualization.Dispose();
        }

        public void ApplyDamage(float amount)
        {
            _health.Lose(amount);
            if (_health.IsOver)
            {
                _shipVisualization.PlayExplosionAnimation();
                _shipVisualization.DisposeOnAnimationEnd();
                _destroyer.Destroy(this);
            }
        }

        public void RestoreHealth(float amount)
        {
            _health.Restore(amount);
        }
    }
}