using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class SettingsMenu : Element, ISettingsMenu
    {
        [SerializeField] private NumberField _maxShips;
        [SerializeField] private NumberField _asteroidsSpawnRate;
        [SerializeField] private ButtonElement _closeButton;
        
        public void Execute(float deltaTime)
        {
            if (_closeButton.Clicked)
            {
                IsActive = false;
                _closeButton.Release();
            }
        }

        public INumberField MaxShipsOnScreen => _maxShips;
        public INumberField AsteroidsSpawnRate => _asteroidsSpawnRate;
    }
}