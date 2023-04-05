using System;
using Game.Runtime.Ship.Weapons;

namespace Game.Runtime.Ship
{
    [Serializable]
    public struct ShipStats
    {
        public float MaxSpeed;
        public float Acceleration;
        public float Damping;
        public float MaxHealth;
        public WeaponStats WeaponStats;
    }
}