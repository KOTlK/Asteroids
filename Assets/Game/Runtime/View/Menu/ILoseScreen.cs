using Game.Runtime.GameLoop;
using Game.Runtime.View.Score;

namespace Game.Runtime.View.Menu
{
    public interface ILoseScreen : IElement, ILoop
    {
        IScoreView Score { get; }
        IButton Restart { get; }
        IButton ExitToMenu { get; }
        IButton ExitGame { get; }
    }
}