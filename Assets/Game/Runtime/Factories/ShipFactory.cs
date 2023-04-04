using System;
using System.Linq;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class ShipFactory : MonoBehaviour, IShipFactory
    {
        [SerializeField] private ShipReference[] _shipReferences;
        [SerializeField] private ShipInterface _interface;
        [SerializeField] private Viewport _viewport;
        [SerializeField] private BulletsFactory _bulletsFactory;

        private ICollidersWorld<IDamageable> _collidersWorld;

        private readonly ExecutableObjectDestroyer<Ship.Ship> _destroyer = new();

        public void Init(ICollidersWorld<IDamageable> collidersWorld)
        {
            _collidersWorld = collidersWorld;
        }

        public Ship.Ship Create(ShipType type, Vector3 position, IShipInput input)
        {
            var reference = _shipReferences.First(shipReference => shipReference.Type == type);
            var view = Instantiate(reference.View);
            var collider = new AABBCollider(new AABB()
            {
                Size = new Vector3(1, 1),
                Center = position,
            });
            
            var model =  new Ship.Ship(
                new ShipVisualization(
                    view,
                    _interface),
                collider,
                new Health(reference.Stats.MaxHealth),
                input,
                reference.Stats,
                _viewport,
                new StandardWeapon(
                    Vector3.up,
                    _bulletsFactory,
                    view,
                    reference.WeaponStats));

            _collidersWorld.Add(collider, model);
            _destroyer.Add(model);

            return model;
        }

        public void Destroy(Ship.Ship model)
        {
            _destroyer.Destroy(model);
            _collidersWorld.Remove(model);
        }

        public void Execute(float deltaTime) => _destroyer.Execute(deltaTime);
    }

    [Serializable]
    public class ShipReference
    {
        /// <summary>
        /// Field, used for inspector
        /// </summary>
        public string Name;
        public ShipType Type;
        public StandardShip View;
        public ShipStats Stats;
        public WeaponStats WeaponStats;
    }
}