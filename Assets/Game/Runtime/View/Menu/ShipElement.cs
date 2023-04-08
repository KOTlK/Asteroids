using Game.Runtime.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.View.Menu
{
    public class ShipElement : ButtonElement, IShipElement
    {
        [SerializeField] private ShipReference _reference;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Image _image;

        public ShipReference Type => _reference;
        
        public void StartHighlighting()
        {
            _image.color = _selectedColor;
        }

        public void StopHighlighting()
        {
            _image.color = _defaultColor;
        }
    }
}