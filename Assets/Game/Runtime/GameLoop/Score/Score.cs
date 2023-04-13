using Game.Runtime.View.Score;

namespace Game.Runtime.GameLoop.Score
{
    public class Score : IScore
    {
        private readonly IScoreView _view;
        
        private int _current;

        public Score(IScoreView view) : this(0, view)
        {
        }

        public Score(int current, IScoreView view)
        {
            _current = current;
            _view = view;
            _view.ShowScore(current);
        }

        public void Add(int amount)
        {
            _current += amount;
            _view.ShowScore(_current);
        }

        public void Visualize(IScoreView view)
        {
            view.ShowScore(_current);
        }
    }
}