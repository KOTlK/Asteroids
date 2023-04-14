using Game.Runtime.Factories;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class StandardWeapon : IWeapon
    {
        private readonly Vector3 _shootDirection;
        private readonly IBulletsFactory _bulletsFactory;
        private readonly IWeaponSlot _slot;
        private readonly WeaponStats _stats;

        private float _reloadElapsedTime;

        public StandardWeapon(Vector3 shootDirection, IBulletsFactory bulletsFactory, IWeaponSlot slot, WeaponStats stats)
        {
            _shootDirection = shootDirection;
            _bulletsFactory = bulletsFactory;
            _slot = slot;
            _stats = stats;
            _reloadElapsedTime = stats.ReloadTime;
        }

        public bool CanShoot => _reloadElapsedTime >= _stats.ReloadTime;

        public void Shoot()
        {
            var bullet = _bulletsFactory.Create(_slot.Pivot, _stats.Damage, _stats.BulletsSpeed);
            bullet.Shoot(_shootDirection);
            _reloadElapsedTime = 0f;
        }

        public void Execute(float deltaTime)
        {
            _reloadElapsedTime += deltaTime;
        }
    }
}