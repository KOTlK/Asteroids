using System;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public interface IShipView : IDisposable
    {
        Vector3 Position { get; set; }
    }
}