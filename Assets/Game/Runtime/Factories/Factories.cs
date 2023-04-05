using System;
using Game.Runtime.Factories.View;

namespace Game.Runtime.Factories
{
    [Serializable]
    public class Factories
    {
        public PlayerShipViewFactory PlayerShipViewFactory;
        public BulletViewFactory EnemyBulletsViewFactory;
        public BulletViewFactory PlayerBulletsViewFactory;
        public AsteroidViewFactory AsteroidViewFactory;
        public EnemyShipViewFactory EnemyShipViewFactory;
    }
}