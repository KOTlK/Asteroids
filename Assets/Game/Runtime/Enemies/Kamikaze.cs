using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;

namespace Game.Runtime.Enemies
{
    public class Kamikaze : IKamikaze
    {
        private readonly ICollider _collider;
        private readonly IColliderCaster<IDamageable> _colliderCaster;
        private readonly float _damage;

        public Kamikaze(ICollider collider, IColliderCaster<IDamageable> colliderCaster, float damage)
        {
            _collider = collider;
            _colliderCaster = colliderCaster;
            _damage = damage;
            Destroyed = false;
        }

        public bool Destroyed { get; private set; }
        
        public void Execute(float deltaTime)
        {
            var collision = _colliderCaster.Cast(_collider);

            if (collision.Occure)
            {
                collision.Target.ApplyDamage(_damage);
                Destroyed = true;
            }
        }
    }
}