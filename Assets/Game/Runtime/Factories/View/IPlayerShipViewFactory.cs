using Game.Runtime.Ship;

namespace Game.Runtime.Factories.View
{
    public interface IPlayerShipViewFactory
    {
        StandardShip Create(ShipType type);
    }
}