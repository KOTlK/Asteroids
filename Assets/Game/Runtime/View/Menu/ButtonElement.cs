using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.View.Menu
{
    [RequireComponent(typeof(Button))]
    public class ButtonElement : Element, IButton
    {
        [SerializeField] private Button _origin;

        [field: SerializeField] public bool Clicked { get; private set; }

        public void Release()
        {
            Clicked = false;
        }

        private void OnEnable()
        {
            _origin.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _origin.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Clicked = true;
        }
    }
}