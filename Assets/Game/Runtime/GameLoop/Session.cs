using Game.Runtime.Enemies;
using Game.Runtime.Factories;
using Game.Runtime.GameLoop.Score;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View;
using Game.Runtime.View.Score;
using UnityEngine;

namespace Game.Runtime.GameLoop
{
    public class Session : ISession
    {
        private readonly Factories.Factories _factories;
        private readonly IViewRoot _viewRoot;
        private readonly IShipInput _playerInput;

        private IDisposableLoop _loop;
        private IShip _player;
        private IScore _score;
        private ShipReference _selectedShip;

        public Session(Factories.Factories factories, IViewRoot viewRoot, IShipInput playerInput)
        {
            _factories = factories;
            _viewRoot = viewRoot;
            _playerInput = playerInput;
        }

        public bool GameLose => _player.IsDead;

        public void Start(ShipReference selectedShip)
        {
            _selectedShip = selectedShip;
            ICollidersWorld<IBullet> bulletsCollidersWorld;
            ICollidersWorld<IDamageable> shipCollidersWorld;
            IColliderCaster<IDamageable> shipColliderCaster;
            ICollidersWorld<IDamageableTarget> enemiesCollidersWorld;
            IBulletsFactory enemyBulletsFactory;
            IEnemiesFactory enemiesFactory;
            IAsteroidsFactory asteroidsFactory;
            IBulletsFactory playerBulletsFactory;
            IPlayerShipFactory shipFactory;
            var enemiesColliderCaster = new ColliderCaster<IDamageableTarget>(enemiesCollidersWorld = new CollidersWorld<IDamageableTarget>());

            _loop = new DisposableGameObjectsGroup(new ILoop[]
            {
                enemiesFactory = new EnemiesFactory(
                    _factories.EnemyShipViewFactory,
                    enemyBulletsFactory = new BulletsFactory(
                        bulletsCollidersWorld = new CollidersWorld<IBullet>(),
                        shipColliderCaster = new ColliderCaster<IDamageable>(
                            shipCollidersWorld = new CollidersWorld<IDamageable>()),
                        _factories.EnemyBulletsViewFactory),
                    enemiesCollidersWorld,
                    shipColliderCaster,
                    _viewRoot.Viewport),
                enemyBulletsFactory,
                shipFactory = new PlayerShipFactory(
                    playerBulletsFactory = new PlayerBulletsFactory(
                        bulletsCollidersWorld,
                        enemiesColliderCaster,
                        _factories.PlayerBulletsViewFactory,
                        _score = new Score.Score(_viewRoot.InGameView)),
                    shipCollidersWorld,
                    _factories.PlayerShipViewFactory,
                    _viewRoot),
                playerBulletsFactory,
                asteroidsFactory = new AsteroidFactory(
                    _viewRoot.Viewport,
                    shipColliderCaster,
                    enemiesCollidersWorld,
                    _factories.AsteroidViewFactory),
                new AsteroidsSpawner(
                    asteroidsFactory,
                    _viewRoot.Viewport),
                new EnemiesSpawner(
                    _viewRoot.MainMenu.Settings.AsteroidsSpawnRate.Content,
                    enemiesFactory,
                    _viewRoot.Viewport,
                    _viewRoot.MainMenu.Settings.MaxShipsOnScreen.Content)
            });
            
            _player = shipFactory.Create(selectedShip, Vector3.zero, _playerInput);
        }

        public void Restart()
        {
            Dispose();
            Start(_selectedShip);
        }

        public void Execute(float deltaTime)
        {
            _loop.Execute(deltaTime);
        }

        public void Dispose()
        {
            _loop.Dispose();
        }

        public void Visualize(IScoreView view)
        {
            _score.Visualize(view);
        }
    }
}