using System;
using System.Linq;
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
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private ShipReference[] _shipReferences;
        [SerializeField] private ShipInterface _interface;
        [SerializeField] private Viewport _viewport;

        public ShipModel Create(ShipType type, Vector3 position, IShipInput input, ICollidersWorld<IDamageable> collidersWorld)
        {
            var reference = _shipReferences.First(shipReference => shipReference.Type == type);
            var view = Instantiate(reference.View);
            var collider = new AABBCollider(new AABB()
            {
                Size = new Vector3(1, 1),
                Center = position,
            });
            
            var model =  new ShipModel(
                new ShipVisualization(
                    view,
                    _interface),
                collider,
                new Health(reference.Stats.MaxHealth),
                input,
                reference.Stats,
                _viewport);

            collidersWorld.Add(collider, model);

            return model;
        }

        public void Destroy(ShipModel model)
        {
            model.Dispose();
        }
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
    }
}