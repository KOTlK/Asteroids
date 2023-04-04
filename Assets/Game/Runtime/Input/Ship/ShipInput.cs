using System;
using UnityEngine;

namespace Game.Runtime.Input.Ship
{
    public class ShipInput : MonoBehaviour, IShipInput
    {
        public bool ShootingMainGun => UnityEngine.Input.GetKey(KeyCode.Space);

        public Vector2 MovementDirection
        {
            get
            {
                var x = UnityEngine.Input.GetAxisRaw("Horizontal");
                var y = UnityEngine.Input.GetAxisRaw("Vertical");

                return new Vector2(x, y);
            }
        }
    }
}