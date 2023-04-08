using Game.Runtime.View.Menu;
using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.View
{
    public class ViewRoot : MonoBehaviour, IViewRoot
    {
        [SerializeField] private ShipInterface _shipInterface;
        [SerializeField] private Viewport.Viewport _viewport;
        [SerializeField] private MainMenu _mainMenu;
        
        public IShipInterface ShipInterface => _shipInterface;
        public IMainMenu MainMenu => _mainMenu;
        public IViewport Viewport => _viewport;
    }
}