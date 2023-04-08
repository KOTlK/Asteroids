using Game.Runtime.GameLoop;

namespace Game.Runtime.View.Menu
{
    public interface IPickShipMenu : IElement, ILoop
    {
        IShipElement Selected { get; }
    }
}