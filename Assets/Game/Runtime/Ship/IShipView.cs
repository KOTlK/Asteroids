using System;
using Game.Runtime.Physics;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public interface IShipView : IDisposable, IRigidbody
    {
        Vector3 Position { get; set; }
    }
}