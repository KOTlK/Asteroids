using System;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public interface IBulletView : IDisposable, IAnimationEndDisposable
    {
        Vector3 Position { get; set; }
        void PlayHitAnimation();
    }
}