using Game.Runtime.Factories;
using Game.Runtime.GameLoop;
using Game.Runtime.View.Viewport;
using UnityEngine;
using Random = System.Random;

namespace Game.Runtime.Enemies
{
    public class EnemiesSpawner : ILoop
    {
        private readonly float _spawnRate;
        private readonly IEnemiesFactory _enemiesFactory;
        private readonly IViewport _viewport;
        private readonly Random _random = new Random();

        public EnemiesSpawner(float spawnRate, IEnemiesFactory enemiesFactory, IViewport viewport)
        {
            _spawnRate = spawnRate;
            _enemiesFactory = enemiesFactory;
            _viewport = viewport;
        }

        private float _timePassed;
        
        public void Execute(float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed >= _spawnRate)
            {
                _timePassed = 0;
                var x = (float)_random.NextDouble();
                var position = _viewport.ViewportToWorld(new Vector3(x, 1));
                position.z = 0;
                _enemiesFactory.Create(position);
            }
        }
    }
}