using UnityEngine;

namespace Game.Runtime.Ship
{
    public class StandardShip : MonoBehaviour, IShipView
    {
        [SerializeField] private Transform _gunPivot;
        [SerializeField] private Animator _animator;

        private readonly int _explosionHash = Animator.StringToHash("Explosion");

        private bool _destroyOnAnimationEnd = false;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 MainGunPivot => _gunPivot.position;
        public void PlayExplosionAnimation()
        {
            _animator.Play(_explosionHash);
        }

        public void Dispose()
        {
            if (_destroyOnAnimationEnd == false)
            {
                Destroy(gameObject);
            }
        }

        public virtual void DisposeOnAnimationEnd()
        {
            _destroyOnAnimationEnd = true;
        }

        public void Destroy()
        {
            if (_destroyOnAnimationEnd)
            {
                Destroy(gameObject);
            }
        }
    }
}