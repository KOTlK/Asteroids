using System;
using Game.Runtime.Physics;
using UnityEngine;

namespace Game.Runtime.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StandardShip : MonoBehaviour, IShipView
    {
        public Type Origin => typeof(ShipModel);

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
        }

    }
}