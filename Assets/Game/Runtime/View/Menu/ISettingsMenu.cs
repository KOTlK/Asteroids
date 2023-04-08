using Game.Runtime.GameLoop;

namespace Game.Runtime.View.Menu
{
    public interface ISettingsMenu : IElement, ILoop
    {
        INumberField MaxShipsOnScreen { get; }
        INumberField AsteroidsSpawnRate { get; }
    }
}