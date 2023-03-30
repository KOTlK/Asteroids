using System;
using UnityEngine;

namespace Game.Runtime.Input.Ship
{
    public interface IShipInput
    {
        event Action MachineGunShoot;
        event Action LaserShoot;
        Vector2 MovementDirection { get; }
    }
}