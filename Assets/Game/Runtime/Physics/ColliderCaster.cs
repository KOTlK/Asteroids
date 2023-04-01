namespace Game.Runtime.Physics
{
    public class ColliderCaster<T> : IColliderCaster<T>
    {
        private readonly ICollidersWorld<T> _world;

        public ColliderCaster(ICollidersWorld<T> world)
        {
            _world = world;
        }

        public Collision<T> Cast(ICollider collider)
        {
            foreach (var target in _world)
            {
                if (target.Cast(collider))
                {
                    return new Collision<T>()
                    {
                        Occure = true,
                        Target = _world[target]
                    };
                }
            }

            return new Collision<T>()
            {
                Occure = false
            };
        }
    }
}