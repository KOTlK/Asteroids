using System;
using Game.Runtime.GameLoop;
using Game.Runtime.GameLoop.Score;
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
        private readonly IScore _score;

        public EnemyShip(IShip origin, IShipView shipVisualization, IObjectDestructor<EnemyShip> destructor, IKamikaze kamikaze, IScore score)
        {
            _origin = origin;
            _kamikaze = kamikaze;
            _score = score;
            _shipVisualization = shipVisualization;
            _destructor = destructor;
            ScorePerKill = new Random().Next(1, 10); // Hardcode yeah yeah
        }

        public int ScorePerKill { get; }
        public bool IsDead => _origin.IsDead;

        public void Execute(float deltaTime)
        {
            _origin.Execute(deltaTime);
            _kamikaze.Execute(deltaTime);

            if (_kamikaze.Destroyed)
            {
                _shipVisualization.PlayExplosionAnimation();
                _destructor.Destroy(this);
            }
        }

        public void ApplyDamage(float amount)
        {
            _origin.ApplyDamage(amount);
            if (IsDead)
            {
                _score.Add(ScorePerKill);
                _shipVisualization.PlayExplosionAnimation();
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

        public void Dispose()
        {
            _shipVisualization.Dispose();
            _origin.Dispose();
        }
    }
}