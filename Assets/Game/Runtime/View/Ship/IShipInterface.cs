using Game.Runtime.View.Health;
using UnityEngine;

namespace Game.Runtime.View.Ship
{
    public interface IShipInterface : IHealthView
    {
        void DisplayVelocity(Vector3 velocity);
        void DisplayPosition(Vector3 position);
    }
}