﻿using Game.Runtime.GameLoop;
using Game.Runtime.Ship;
using Game.Runtime.View.Ship;

namespace Game.Runtime.Enemies
{
    public class EnemyShip : IShip
    {
        private readonly IShip _origin;
        private readonly IKamikaze _kamikaze;
        private readonly IShipView _shipVisualization;
        private readonly IObjectDestructor<EnemyShip> _destructor;

        public EnemyShip(IShip origin, IShipView shipVisualization, IObjectDestructor<EnemyShip> destructor, IKamikaze kamikaze)
        {
            _origin = origin;
            _kamikaze = kamikaze;
            _shipVisualization = shipVisualization;
            _destructor = destructor;
        }

        public void Execute(float deltaTime)
        {
            _origin.Execute(deltaTime);
            _kamikaze.Execute(deltaTime);

            if (_kamikaze.Destroyed)
            {
                _shipVisualization.PlayExplosionAnimation();
                _shipVisualization.DisposeOnAnimationEnd();
                _destructor.Destroy(this);
            }
        }

        public void Dispose()
        {
            _shipVisualization.Dispose();
            _origin.Dispose();
        }

        public bool IsDead => _origin.IsDead;

        public void ApplyDamage(float amount)
        {
            _origin.ApplyDamage(amount);
            if (IsDead)
            {
                _shipVisualization.PlayExplosionAnimation();
                _shipVisualization.DisposeOnAnimationEnd();
                _destructor.Destroy(this);
            }
        }

        public void RestoreHealth(float amount)
        {
            _origin.RestoreHealth(amount);
        }

        public void Visualize(IShipInterface view)
        {
            _origin.Visualize(view);
        }
    }
}