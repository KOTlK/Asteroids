using Game.Runtime.GameLoop;
using Game.Runtime.View.Health;
using Game.Runtime.View.Score;

namespace Game.Runtime.View.Menu
{
    public interface IInGameView : IElement, IScoreView, IHealthView
    {
    }
}