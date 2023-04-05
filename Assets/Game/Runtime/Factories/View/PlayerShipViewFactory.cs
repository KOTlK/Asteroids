using System;
using System.Linq;
using Game.Runtime.Ship;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class PlayerShipViewFactory : MonoBehaviour, IPlayerShipViewFactory
    {
        [SerializeField] private ShipViewLink[] _shipReferences;

        public StandardShip Create(ShipType type)
        {
            var reference = _shipReferences.First(shipReference => shipReference.Type == type);
            return Instantiate(reference.View);
        }
    }
    
    [Serializable]
    public struct ShipViewLink
    {
        /// <summary>
        /// Field, used for inspector
        /// </summary>
        public string Name;
        public ShipType Type;
        public StandardShip View;
    }
}