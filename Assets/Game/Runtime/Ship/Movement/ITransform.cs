using UnityEngine;

namespace Game.Runtime.Ship.Movement
{
    public interface ITransform
    {
        Vector3 Position { get; set; }
    }
}