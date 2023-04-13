using Game.Runtime.View;
using Game.Runtime.View.Score;

namespace Game.Runtime.GameLoop.Score
{
    public interface IScore : IVisualization<IScoreView>
    {
        void Add(int amount);
    }
}