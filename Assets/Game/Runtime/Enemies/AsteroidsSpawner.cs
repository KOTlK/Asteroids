using Game.Runtime.Factories;
using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;
using Random = System.Random;

namespace Game.Runtime.Enemies
{
    public class AsteroidsSpawner : ILoop
    {
        private readonly IAsteroidsFactory _factory;
        private readonly IColliderCaster<IDamageable> _colliderCaster;
        private readonly IViewport _viewport;
        private readonly Random _random;

        private float _timePassed;
        private float _delay;

        public AsteroidsSpawner(IAsteroidsFactory factory, IColliderCaster<IDamageable> colliderCaster, IViewport viewport)
        {
            _random = new Random();
            _factory = factory;
            _colliderCaster = colliderCaster;
            _viewport = viewport;
            _delay = _random.Next(0, 7);
        }

        public void Execute(float deltaTime)
        {
            _timePassed += deltaTime;

            if (_timePassed >= _delay)
            {
                var x = _random.NextDouble();
                var speed = _random.Next(2, 8);
                var damage = _random.Next(10, 20);
                var position = new Vector3((float)x, 1.5f);
                var worldPosition = _viewport.ViewportToWorld(position);
                worldPosition.z = 0;

                _factory.Create(_colliderCaster, speed, damage, worldPosition);
                _delay = _random.Next(3, 7);
                _timePassed = 0;
            }
        }
    }
}