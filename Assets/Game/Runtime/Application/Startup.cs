using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
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
            var shipCollisions = new CollidersWorld<ShipModel>();
            var shipCollider = new AABBCollider(new AABB()
            {
                Size = new Vector3(1, 1),
                Center = Vector3.zero
            });
            var shipColliderCaster = new ColliderCaster<ShipModel>(shipCollisions);
            var shipModel = _factories.ShipFactory.Create(ShipType.Fast, _shipInput, shipCollider);
            shipCollisions.Add(shipCollider, shipModel);

            var bullet =
                _factories.BulletsFactory.Create(new Vector3(0, 10), shipColliderCaster);
            bullet.Shoot(Vector3.down);

            _loop = new GameObjectsLoop(new ILoop[]
            {
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