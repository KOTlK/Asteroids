using UnityEngine;

namespace Game.Runtime.Enemies
{
    public class AsteroidView : MonoBehaviour, IAsteroidView
    {
        [SerializeField] private Animator _animator;

        private bool _disposeOnAnimationEnd = false;

        private readonly int _explosionHash = Animator.StringToHash("Explosion");

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void PlayExplosionAnimation()
        {
            _animator.Play(_explosionHash);
        }

        public void DisposeOnAnimationEnd()
        {
            _disposeOnAnimationEnd = true;
        }

        public void Dispose()
        {
            if (_disposeOnAnimationEnd == false)
            {
                Destroy();
                Debug.Log("Destroyed");
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}