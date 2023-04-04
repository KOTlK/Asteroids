using Game.Runtime.Enemies;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class AsteroidFactory : MonoBehaviour, IAsteroidsFactory
    {
        [SerializeField] private AsteroidView[] _asteroidsViews;
        [SerializeField] private float _radius;
        [SerializeField] private Viewport _viewport;

        private readonly ExecutableObjectDestroyer<Asteroid> _destroyer = new();

        public Asteroid Create(IColliderCaster<IDamageable> colliderCaster, float speed, float damage, Vector3 startPosition)
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

            _destroyer.Add(asteroid);
            
            return asteroid;
        }

        public void Destroy(Asteroid obj) => _destroyer.Destroy(obj);

        public void Execute(float deltaTime) => _destroyer.Execute(deltaTime);
    }
}