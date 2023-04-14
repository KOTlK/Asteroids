using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;

namespace Game.Runtime.Enemies
{
    public class Kamikaze : IKamikaze
    {
        private readonly IBody<IDamageable> _body;
        private readonly float _damage;

        public Kamikaze(IBody<IDamageable> body, float damage)
        {
            _body = body;
            _damage = damage;
            Destroyed = false;
        }

        public bool Destroyed { get; private set; }
        
        public void Execute(float deltaTime)
        {
            var collision = _body.Cast();

            if (collision.Occure)
            {
                collision.Target.ApplyDamage(_damage);
                Destroyed = true;
            }
        }
    }
}