using System;
using Game.Runtime.GameLoop;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public interface IBullet : IDisposable, ILoop
    {
        void Shoot(Vector3 direction);
    }
}