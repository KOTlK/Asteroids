using Game.Runtime.Enemies;
using Game.Runtime.Factories.View;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class AsteroidFactory : IAsteroidsFactory
    {
        private readonly ExecutableObjectDestructor<Asteroid> _destructor = new();
        private readonly IViewport _viewport;
        private readonly IColliderCaster<IDamageable> _targetColliders;
        private readonly ICollidersWorld<IDamageable> _asteroidsWorld;
        private readonly IAsteroidViewFactory _asteroidViewFactory;

        private const float Radius = 0.3f;

        public AsteroidFactory(IViewport viewport, IColliderCaster<IDamageable> targetColliders, ICollidersWorld<IDamageable> asteroidsWorld, IAsteroidViewFactory asteroidViewFactory)
        {
            _viewport = viewport;
            _targetColliders = targetColliders;
            _asteroidsWorld = asteroidsWorld;
            _asteroidViewFactory = asteroidViewFactory;
        }

        public Asteroid Create(float speed, float damage, Vector3 startPosition)
        {
            var view = _asteroidViewFactory.Create(startPosition);
            var collider = new CircleCollider(new Circle()
            {
                Center = startPosition,
                Radius = Radius
            });
            var asteroid = new Asteroid(view,
                _viewport,
                this,
                collider,
                _targetColliders,
                speed,
                damage);

            _destructor.Add(asteroid);
            _asteroidsWorld.Add(collider, asteroid);
            
            return asteroid;
        }

        public void Destroy(Asteroid obj)
        {
            _asteroidsWorld.Remove(obj);
            _destructor.Destroy(obj);
        }

        public void Execute(float deltaTime) => _destructor.Execute(deltaTime);
    }
}