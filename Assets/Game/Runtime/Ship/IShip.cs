using System;
using Game.Runtime.GameLoop;
using Game.Runtime.Ship.Weapons;

namespace Game.Runtime.Ship
{
    public interface IShip : ILoop, IDisposable, IDamageable
    {
        void RestoreHealth(float amount);
    }
}