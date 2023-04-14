using System;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public interface IAsteroidView : IDisposable
    {
        Vector3 Position { get; set; }
        void PlayExplosionAnimation();
    }
}