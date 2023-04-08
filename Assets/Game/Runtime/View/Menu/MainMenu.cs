using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class MainMenu : Element, IMainMenu
    {
        [SerializeField] private PickShipMenu _pickShipMenu;
        [SerializeField] private SettingsMenu _settings;
        [SerializeField] private ButtonElement _startGame;
        [SerializeField] private ButtonElement _exitGame;
        [SerializeField] private ButtonElement _openSettings;
        
        public void Execute(float deltaTime)
        {
            if (_openSettings.Clicked)
            {
                if (_settings.IsActive == false)
                {
                    _openSettings.Release();
                    _settings.IsActive = true;
                }
            }
            
            if (_settings.IsActive)
            {
                _settings.Execute(deltaTime);
            }
            
            if (_pickShipMenu.IsActive)
            {
                _pickShipMenu.Execute(deltaTime);
            }
        }

        public ISettingsMenu Settings => _settings;
        public IPickShipMenu ShipPicker => _pickShipMenu;
        public IButton OpenSettingsButton => _openSettings;
        public IButton StartGameButton => _startGame;
        public IButton ExitButton => _exitGame;
    }
}