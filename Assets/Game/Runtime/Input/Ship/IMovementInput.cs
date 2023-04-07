using UnityEngine;

namespace Game.Runtime.Input.Ship
{
    public interface IMovementInput
    {
        Vector3 MovementDirection { get; }
    }
}