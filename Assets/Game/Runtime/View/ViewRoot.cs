using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.View
{
    public class ViewRoot : MonoBehaviour, IViewRoot
    {
        [SerializeField] private ShipInterface _shipInterface;
        [SerializeField] private Viewport.Viewport _viewport;
        
        public IShipInterface ShipInterface => _shipInterface;
        public IViewport Viewport => _viewport;
    }
}