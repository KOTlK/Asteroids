using UnityEngine;

namespace Game.Runtime.Factories.View.Explosions
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _sound;

        private readonly int _explosionHash = Animator.StringToHash("Explosion");

        public bool IsPlaying => _sound.isPlaying || _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
        
        public void Explode(Vector3 position)
        {
            transform.position = position;
            _animator.Play(_explosionHash);
            _sound.Play();
        }
    }
}