using UnityEngine;

namespace Game.Runtime.Ship
{
    public class StandardShip : MonoBehaviour, IShipView
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}