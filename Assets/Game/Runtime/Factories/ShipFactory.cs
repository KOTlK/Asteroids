﻿using System;
using System.Linq;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Hp;
using Game.Runtime.View.Ship;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private ShipReference[] _shipReferences;
        [SerializeField] private ShipInterface _interface;

        public ShipModel Create(ShipType type, IShipInput input, ICollider collider)
        {
            var reference = _shipReferences.First(shipReference => shipReference.Type == type);
            var view = Instantiate(reference.View);
            return new ShipModel(
                new ShipVisualization(
                    view,
                    _interface),
                collider,
                new Health(reference.Stats.MaxHealth),
                input,
                reference.Stats);
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