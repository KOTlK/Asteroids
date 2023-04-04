using System;
using UnityEngine;

namespace Game.Runtime.Input.Ship
{
    public interface IShipInput
    {
        bool ShootingMainGun { get; }
        Vector2 MovementDirection { get; }
    }
}