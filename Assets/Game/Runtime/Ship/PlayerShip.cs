using Game.Runtime.View.Ship;

namespace Game.Runtime.Ship
{
    public class PlayerShip : IShip
    {
        private readonly IShip _origin;
        private readonly ShipVisualization _view;

        public PlayerShip(IShip origin, ShipVisualization view)
        {
            _origin = origin;
            _view = view;
        }

        public void Execute(float deltaTime)
        {
            _origin.Execute(deltaTime);
            _origin.Visualize(_view);
        }

        public void Dispose()
        {
            _view.Dispose();
            _origin.Dispose();
        }

        public bool IsDead => _origin.IsDead;

        public void ApplyDamage(float amount)
        {
            _origin.ApplyDamage(amount);
            _origin.Visualize(_view);
        }

        public void RestoreHealth(float amount)
        {
            _origin.RestoreHealth(amount);
            _origin.Visualize(_view);
        }

        public void Visualize(IShipInterface view) => _origin.Visualize(view);
    }
}