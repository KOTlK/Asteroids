using System;

namespace Game.Runtime.Ship
{
    [Serializable]
    public class ShipStats
    {
        public float MaxSpeed;
        public float Acceleration;
        public float Damping;
        public float MaxHealth;
    }
}