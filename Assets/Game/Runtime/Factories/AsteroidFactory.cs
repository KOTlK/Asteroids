using System.Collections.Generic;
using Game.Runtime.Enemies;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class AsteroidFactory : MonoBehaviour, IAsteroidsFactory
    {
        [SerializeField] private AsteroidView[] _asteroidsViews;
        [SerializeField] private float _radius;
        [SerializeField] private Viewport _viewport;

        private readonly List<Asteroid> _asteroids = new();
        private readonly Queue<Asteroid> _removeQueue = new();

        public Asteroid Create(IColliderCaster<ShipModel> colliderCaster, float speed, float damage, Vector3 startPosition)
        {
            var random = Random.Range(0, _asteroidsViews.Length - 1);
            var view = Instantiate(_asteroidsViews[random], startPosition, Quaternion.identity);
            var asteroid = new Asteroid(view,
                _viewport,
                this,
                new CircleCollider(
                    new Circle()
                    {
                        Center = startPosition,
                        Radius = _radius
                    }),
                colliderCaster,
                speed,
                damage);

            _asteroids.Add(asteroid);
            
            return asteroid;
        }
        
        public void Destroy(Asteroid obj)
        {
            _removeQueue.Enqueue(obj);
        }

        public void Execute(float deltaTime)
        {
            while (_removeQueue.Count > 0)
            {
                var asteroid = _removeQueue.Dequeue();

                asteroid.Dispose();
                _asteroids.Remove(asteroid);
            }

            foreach (var asteroid in _asteroids)
            {
                asteroid.Execute(deltaTime);
            }
        }
    }
}