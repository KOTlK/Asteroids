using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Ship;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public interface IShipFactory : IObjectDestructor<Ship.Ship>, ILoop
    {
        Ship.Ship Create(ShipType type, Vector3 position, IShipInput input);
    }
}