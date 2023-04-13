using Game.Runtime.View.Health;
using Game.Runtime.View.Score;
using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class InGameView : Element, IInGameView
    {
        [SerializeField] private ScoreView _score;
        [SerializeField] private HealthView _healthView;

        public void ShowScore(int score) => _score.ShowScore(score);

        public void DisplayHealth(float amount) => _healthView.DisplayHealth(amount);
    }
}