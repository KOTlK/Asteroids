using Game.Runtime.GameLoop;
using Game.Runtime.View;
using Game.Runtime.View.Ship;

namespace Game.Runtime.Ship.Movement
{
    public interface IShipMovement : ILoop, ITransform, IVisualization<IShipInterface>
    {
    }
}