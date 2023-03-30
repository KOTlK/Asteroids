using System;
using UnityEngine;

namespace Game.Runtime.Input.Ship
{
    public class ShipInput : MonoBehaviour, IShipInput
    {
        public event Action MachineGunShoot = delegate {  };
        public event Action LaserShoot = delegate {  };

        public Vector2 MovementDirection
        {
            get
            {
                var x = UnityEngine.Input.GetAxisRaw("Horizontal");
                var y = UnityEngine.Input.GetAxisRaw("Vertical");

                return new Vector2(x, y);
            }
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                MachineGunShoot();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
            {
                LaserShoot();
            }
        }
    }
}