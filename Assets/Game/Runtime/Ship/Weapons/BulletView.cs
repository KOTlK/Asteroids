using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class BulletView : MonoBehaviour, IBulletView
    {
        [SerializeField] private Animator _animator;

        private int _explosionHash = Animator.StringToHash("Explosion");
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void PlayHitAnimation()
        {
            _animator.Play(_explosionHash);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}