using Game.Runtime.View.Score;
using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class LoseScreen : Element, ILoseScreen
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ButtonElement _restart;
        [SerializeField] private ButtonElement _exitToMenu;
        [SerializeField] private ButtonElement _exitGame;

        public IScoreView Score => _scoreView;
        public IButton Restart => _restart;
        public IButton ExitToMenu => _exitToMenu;
        public IButton ExitGame => _exitGame;
        
        public void Execute(float deltaTime)
        {
        }
    }
}