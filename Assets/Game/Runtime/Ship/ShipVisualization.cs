using System;
using Game.Runtime.Ship.Hp;
using Game.Runtime.View.Ship;
using UnityEngine;

namespace Game.Runtime.Ship
{
    public class ShipVisualization : IShipView, IShipInterface
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

        public Vector3 MainGunPivot => _view.MainGunPivot;
        public void PlayExplosionAnimation() => _view.PlayExplosionAnimation();

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

        public void DisposeOnAnimationEnd() => _view.DisposeOnAnimationEnd();
        public void DisplayHealth(float amount) => _shipInterface.DisplayHealth(amount);

        public void DisplayVelocity(Vector3 velocity) => _shipInterface.DisplayVelocity(velocity);

        public void DisplayPosition(Vector3 position) => _shipInterface.DisplayPosition(position);
    }
}