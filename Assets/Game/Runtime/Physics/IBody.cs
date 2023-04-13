using Game.Runtime.Ship.Movement;

namespace Game.Runtime.Physics
{
    public interface IBody<T> : ITransform
    {
        Collision<T> Cast();
    }
}