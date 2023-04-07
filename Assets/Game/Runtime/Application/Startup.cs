using System.Collections.Generic;
using Game.Runtime.Enemies;
using Game.Runtime.Factories;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Application
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Factories.Factories _factories;
        [SerializeField] private ShipInput _shipInput;
        [SerializeField] private ViewRoot _viewRoot;
        [SerializeField] private ShipReference[] _shipsSettings;

        private ILoop _loop;

        private void Awake()
        {
            var bulletsCollidersWorld = new CollidersWorld<IBullet>();
            var shipCollidersWorld = new CollidersWorld<IDamageable>();
            var shipColliderCaster = new ColliderCaster<IDamageable>(shipCollidersWorld);
            var enemiesCollidersWorld = new CollidersWorld<IDamageable>();
            var enemiesColliderCaster = new ColliderCaster<IDamageable>(enemiesCollidersWorld);
            var shipReferences = new Dictionary<ShipType, ShipReference>();
            IBulletsFactory enemyBulletsFactory;

            FillReferences(shipReferences);

            var enemiesFactory = new EnemiesFactory(
                _factories.EnemyShipViewFactory,
                enemyBulletsFactory = new BulletsFactory(
                    bulletsCollidersWorld,
                    shipColliderCaster,
                    _factories.EnemyBulletsViewFactory),
                enemiesCollidersWorld,
                shipColliderCaster,
                _viewRoot.Viewport);
            
            var asteroidsFactory = new AsteroidFactory(
                _viewRoot.Viewport,
                shipColliderCaster,
                enemiesCollidersWorld,
                _factories.AsteroidViewFactory);
            
            var playerBulletsFactory = new BulletsFactory(
                bulletsCollidersWorld,
                enemiesColliderCaster,
                _factories.PlayerBulletsViewFactory);

            var shipFactory = new PlayerShipFactory(
                playerBulletsFactory,
                shipCollidersWorld,
                _factories.PlayerShipViewFactory,
                _viewRoot,
                shipReferences);

            shipFactory.Create(ShipType.Fast, Vector3.zero, _shipInput);

            _loop = new GameObjectsLoop(new ILoop[]
            {
                enemiesFactory,
                enemyBulletsFactory,
                playerBulletsFactory,
                asteroidsFactory,
                shipFactory,
                new AsteroidsSpawner(
                    asteroidsFactory,
                    _viewRoot.Viewport),
                new EnemiesSpawner(2f,
                    enemiesFactory,
                    _viewRoot.Viewport)
            });
        }

        private void Update()
        {
            _loop.Execute(Time.deltaTime);
        }

        private void FillReferences(IDictionary<ShipType, ShipReference> dict)
        {
            foreach (var reference in _shipsSettings)
            {
                dict.Add(reference.Type, reference);
            }
        }
    }
}