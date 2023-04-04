using System;

namespace Game.Runtime.Factories
{
    [Serializable]
    public class Factories
    {
        public ShipFactory ShipFactory;
        public BulletsFactory EnemyBulletsFactory;
        public BulletsFactory PlayerBulletsFactory;
        public AsteroidFactory AsteroidFactory;
        public EnemiesFactory EnemiesFactory;
    }
}