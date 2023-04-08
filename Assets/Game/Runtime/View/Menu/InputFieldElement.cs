using TMPro;
using UnityEngine;

namespace Game.Runtime.View.Menu
{
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldElement : Element, IInputField
    {
        [SerializeField] private TMP_InputField _origin;

        public string Content => _origin.text;
    }
}