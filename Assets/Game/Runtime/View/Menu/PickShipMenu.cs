using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class PickShipMenu : Element, IPickShipMenu
    {
        [SerializeField] private ShipElement[] _elements;

        public IShipElement Selected { get; private set; }

        public void Execute(float deltaTime)
        {
            foreach (var element in _elements)
            {
                if (element.Clicked == false) continue;

                Selected.StopHighlighting();
                Selected = element;
                Selected.StartHighlighting();
                element.Release();
                break;
            }
        }

        private void OnEnable()
        {
            Selected?.StopHighlighting();
            Selected = _elements[0];
            Selected.StartHighlighting();
        }
    }
}