using System;
using Game.Runtime.Ship.Weapons;

namespace Game.Runtime.Enemies
{
    [Serializable]
    public struct EnemyShipStats
    {
        public float Speed;
        public float DamageOnCollision;
        public float MaxHealth;
        public WeaponStats WeaponStats;
    }
}