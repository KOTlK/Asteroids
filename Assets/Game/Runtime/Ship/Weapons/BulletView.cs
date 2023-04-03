using UnityEngine;

namespace Game.Runtime.Ship.Weapons
{
    public class BulletView : MonoBehaviour, IBulletView
    {
        [SerializeField] private Animator _animator;

        private bool _disposeOnAnimationEnd = false;

        private readonly int _explosionHash = Animator.StringToHash("Explosion");
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void PlayHitAnimation()
        {
            _animator.Play(_explosionHash);
        }

        public void DisposeOnAnimationEnd()
        {
            _disposeOnAnimationEnd = true;
        }

        public void Dispose()
        {
            if (_disposeOnAnimationEnd)
                return;
            
            Destroy(gameObject);
        }

        public void Destroy()
        {
            if (_disposeOnAnimationEnd)
            {
                Destroy(gameObject);
            }
        }
    }
}