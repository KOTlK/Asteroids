using System;
using Game.Runtime.GameLoop;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View;
using Game.Runtime.View.Ship;

namespace Game.Runtime.Ship
{
    public interface IShip : ILoop, IDisposable, IDamageable, IVisualization<IShipInterface>
    {
        void RestoreHealth(float amount);
    }
}