using System;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public interface IAsteroidView : IDisposable, IAnimationEndDisposable
    {
        Vector3 Position { get; set; }
        void PlayExplosionAnimation();
    }
}