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
            var shipCollisions = new CollidersWorld<IDamageable>();
            var shipColliderCaster = new ColliderCaster<IDamageable>(shipCollisions);
            var enemiesCollisionsWorld = new CollidersWorld<IDamageable>();

            var shipModel = _factories.ShipFactory.Create(ShipType.Fast, Vector3.zero, _shipInput, shipCollisions);
            _factories.EnemiesFactory.Create(new Vector3(0, 10), enemiesCollisionsWorld, shipColliderCaster);

            _loop = new GameObjectsLoop(new ILoop[]
            {
                _factories.EnemiesFactory,
                _factories.BulletsFactory,
                _factories.AsteroidFactory,
                new AsteroidsSpawner(
                    _factories.AsteroidFactory,
                    shipColliderCaster,
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