using TMPro;
using UnityEngine;

namespace Game.Runtime.View.Ship
{
    public class ShipInterface : MonoBehaviour, IShipInterface
    {
        [SerializeField] private TMP_Text _velocity;
        [SerializeField] private TMP_Text _position;
        
        public void DisplayVelocity(Vector3 velocity)
        {
            _velocity.text = $"{velocity}";
        }

        public void DisplayPosition(Vector3 position)
        {
            _position.text = $"{position}";
        }
    }
}