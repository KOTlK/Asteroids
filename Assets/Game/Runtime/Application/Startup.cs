using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Application
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Factories.Factories _factories;
        [SerializeField] private ShipInput _shipInput;
        [SerializeField] private Viewport _viewport;

        private ILoop _loop;

        private void Awake()
        {
            var bulletsCollidersWorld = new CollidersWorld<IBullet>();
            var shipCollidersWorld = new CollidersWorld<IDamageable>();
            var shipColliderCaster = new ColliderCaster<IDamageable>(shipCollidersWorld);
            var enemiesCollidersWorld = new CollidersWorld<IDamageable>();
            var enemiesColliderCaster = new ColliderCaster<IDamageable>(enemiesCollidersWorld);
            
            _factories.EnemyBulletsFactory.Init(bulletsCollidersWorld, shipColliderCaster);
            _factories.PlayerBulletsFactory.Init(bulletsCollidersWorld, enemiesColliderCaster);
            _factories.AsteroidFactory.Init(shipColliderCaster, enemiesCollidersWorld);
            _factories.EnemiesFactory.Init(enemiesCollidersWorld, shipColliderCaster);
            _factories.ShipFactory.Init(shipCollidersWorld);

            var shipModel = _factories.ShipFactory.Create(ShipType.Fast, Vector3.zero, _shipInput);
            _factories.EnemiesFactory.Create(new Vector3(0, 10));

            _loop = new GameObjectsLoop(new ILoop[]
            {
                _factories.EnemiesFactory,
                _factories.EnemyBulletsFactory,
                _factories.PlayerBulletsFactory,
                _factories.AsteroidFactory,
                new AsteroidsSpawner(
                    _factories.AsteroidFactory,
                    _viewport),
                shipModel
            });
        }

        private void Update()
        {
            _loop.Execute(Time.deltaTime);
        }
    }
}