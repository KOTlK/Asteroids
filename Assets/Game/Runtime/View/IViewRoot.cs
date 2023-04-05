using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;

namespace Game.Runtime.View
{
    public interface IViewRoot
    {
        IShipInterface ShipInterface { get; }
        IViewport Viewport { get; }
    }
}