using Game.Runtime.GameLoop;

namespace Game.Runtime.Input.Ship
{
    public interface IEnemyShipInput : IShipInput, ILoop
    {
        void ReverseDirection();
    }
}