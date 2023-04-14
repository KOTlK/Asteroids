using Game.Runtime.Factories.View.Explosions;
using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class BulletView : MonoBehaviour, IBulletView
    {
        [SerializeField] private AudioSource _shootSound;
        
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

        public void PlayHitAnimation()
        {
            _explosionsFactory.Create().Explode(Position);
        }

        public void PlayShootSound()
        {
            _shootSound.Play();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}