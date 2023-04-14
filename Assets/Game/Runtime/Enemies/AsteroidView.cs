using Game.Runtime.Factories.View.Explosions;
using UnityEngine;

namespace Game.Runtime.Enemies
{
    public class AsteroidView : MonoBehaviour, IAsteroidView
    {
        private ExplosionsFactory _explosionsFactory;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void Init(ExplosionsFactory explosionsFactory)
        {
            _explosionsFactory = explosionsFactory;
        }

        public void PlayExplosionAnimation()
        {
            _explosionsFactory.Create().Explode(transform.position);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}