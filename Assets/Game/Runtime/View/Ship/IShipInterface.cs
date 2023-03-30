using UnityEngine;

namespace Game.Runtime.View.Ship
{
    public interface IShipInterface
    {
        void DisplayVelocity(Vector3 velocity);
        void DisplayPosition(Vector3 position);
    }
}