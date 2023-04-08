using TMPro;
using UnityEngine;

namespace Game.Runtime.View.Menu
{
    [RequireComponent(typeof(TMP_InputField))]
    public class NumberField : Element, INumberField
    {
        [SerializeField] private TMP_InputField _origin;

        public int Content => int.Parse(_origin.text);
    }
}