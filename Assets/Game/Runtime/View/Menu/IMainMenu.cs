using Game.Runtime.GameLoop;

namespace Game.Runtime.View.Menu
{
    public interface IMainMenu : IElement, ILoop
    {
        ISettingsMenu Settings { get; }
        IPickShipMenu ShipPicker { get; }
        IButton OpenSettingsButton { get; }
        IButton StartGameButton { get; }
        IButton ExitButton { get; }
    }
}