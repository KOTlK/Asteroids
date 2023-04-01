using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Application
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Factories.Factories _factories;
        [SerializeField] private ShipInput _shipInput;

        private ShipModel _shipModel;
        private IBullet _bullet;

        private void Awake()
        {
            var shipCollisions = new CollidersWorld<ShipModel>();
            var shipCollider = new AABBCollider(new AABB()
            {
                Size = new Vector3(1, 1),
                Center = Vector3.zero
            });
            _shipModel = _factories.ShipFactory.Create(ShipType.Fast, _shipInput, shipCollider);
            shipCollisions.Add(shipCollider, _shipModel);

            _bullet =
                _factories.BulletsFactory.Create(new Vector3(0, 10), new ColliderCaster<ShipModel>(shipCollisions));
            _bullet.Shoot(Vector3.down);
        }

        private void Update()
        {
            _shipModel.Execute(Time.deltaTime);
            _bullet.Execute(Time.deltaTime);
        }
    }
}