using System;

namespace Game.Runtime.Enemies
{
    [Serializable]
    public struct EnemyShipStats
    {
        public float Speed;
        public float DamageOnCollision;
        public float Damage;
        public float BulletsSpeed;
        public float MaxHealth;
    }
}