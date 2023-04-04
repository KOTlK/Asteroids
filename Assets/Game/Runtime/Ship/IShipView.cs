using System;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public interface IShipView : IDisposable, IAnimationEndDisposable
    {
        Vector3 Position { get; set; }
        Vector3 MainGunPivot { get; }
        void PlayExplosionAnimation();
    }
}