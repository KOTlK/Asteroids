using System;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public interface IBulletView : IDisposable
    {
        Vector3 Position { get; set; }
        void PlayHitAnimation();
    }
}