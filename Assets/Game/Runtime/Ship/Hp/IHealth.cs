using Game.Runtime.View;
using Game.Runtime.View.Health;

namespace Game.Runtime.Ship.Hp
{
    public interface IHealth : IVisualization<IHealthView>
    {
        bool IsOver { get; }
        void Lose(float amount);
        void Restore(float amount);
    }
}