using System;
using Game.Runtime.Ship.Hp;
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

        public Vector3 Position
        {
            get => _view.Position;
            set => _view.Position = value;
        }

        public void DrawUi(Vector3 velocity, IHealth health)
        {
            _shipInterface.DisplayPosition(_view.Position);
            _shipInterface.DisplayVelocity(velocity);
            health.Visualize(_shipInterface);
        }

        public void Dispose()
        {
            _view.Dispose();
        }
    }
}