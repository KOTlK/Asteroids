using System;

namespace Game.Runtime.Ship.Weapons
{
    [Serializable]
    public struct WeaponStats
    {
        public float Damage;
        public float BulletsSpeed;
        public float ReloadTime;
    }
}