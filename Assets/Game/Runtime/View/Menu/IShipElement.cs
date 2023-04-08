using Game.Runtime.Factories;

namespace Game.Runtime.View.Menu
{
    public interface IShipElement : IButton, IHighlight
    {
        ShipReference Type { get; }
    }
}