using Game.Runtime.Factories.View.Explosions;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public class StandardShip : MonoBehaviour, IShipView
    {
        [SerializeField] private Transform _gunPivot;
        
        private Explosion _explosion;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 Pivot => _gunPivot.position;

        public void Init(Explosion explosion)
        {
            _explosion = explosion;
        }
        
        public void PlayExplosionAnimation()
        {
            _explosion.Explode(Position);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}