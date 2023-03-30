using System;
using Game.Runtime.View.Ship;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public class ShipVisualization : IDisposable
    {
        private readonly IShipView _view;
        private readonly IShipInterface _shipInterface;

        public ShipVisualization(IShipView view, IShipInterface shipInterface)
        {
            _view = view;
            _shipInterface = shipInterface;
        }

        public void Move(Vector3 direction)
        {
            _view.Position += direction;
        }

        public void DrawUi(Vector3 velocity)
        {
            _shipInterface.DisplayPosition(_view.Position);
            _shipInterface.DisplayVelocity(velocity);
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}