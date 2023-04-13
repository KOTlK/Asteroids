using Game.Runtime.View.Menu;
using TMPro;
using UnityEngine;

namespace Game.Runtime.View.Health
{
    public class HealthView : Element, IHealthView
    {
        [SerializeField] private TMP_Text _health;
        
        public void DisplayHealth(float amount)
        {
            _health.text = amount.ToString();
        }
    }
}