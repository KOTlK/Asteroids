using Game.Runtime.Input.Ship;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Movement;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Ship;

namespace Game.Runtime.Ship
{
    public class Ship : IShip
    {
        private readonly IHealth _health;
        private readonly IShipMovement _movement;
        private readonly IShipInput _input;
        private readonly IWeapon _weapon;
        
        public Ship(IHealth health, IShipMovement movement, IShipInput input, IWeapon weapon)
        {
            _health = health;
            _movement = movement;
            _input = input;
            _weapon = weapon;
        }

        public void Execute(float deltaTime)
        {
            _weapon.Execute(deltaTime);
            _movement.Execute(deltaTime);

            if (_input.ShootingMainGun && _weapon.CanShoot)
            {
                _weapon.Shoot();
            }
        }

        public bool IsDead => _health.IsOver;

        public void ApplyDamage(float amount)
        {
            _health.Lose(amount);
        }

        public void RestoreHealth(float amount)
        {
            _health.Restore(amount);
        }

        public void Dispose()
        {
            
        }

        public void Visualize(IShipInterface view)
        {
            _movement.Visualize(view);
            _health.Visualize(view);
        }
    }
}