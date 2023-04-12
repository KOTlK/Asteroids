using TMPro;
using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class NumberField : Element, INumberField
    {
        [SerializeField] private TMP_InputField _origin;

        public int Content => int.Parse(_origin.text);
    }
}