using TMPro;
using UnityEngine;

namespace Game.Runtime.View.Score
{
    public class ScoreView : MonoBehaviour, IScoreView
    {
        [SerializeField] private TMP_Text _score;
        
        public void ShowScore(int score)
        {
            _score.text = score.ToString();
        }
    }
}