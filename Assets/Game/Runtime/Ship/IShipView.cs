using System;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public interface IShipView : IDisposable, IAnimationEndDisposable, IWeaponSlot
    {
        Vector3 Position { get; set; }
        void PlayExplosionAnimation();
    }
}