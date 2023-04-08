using UnityEngine;

namespace Game.Runtime.View.Menu
{
    public class Element : MonoBehaviour, IElement
    {
        public bool IsActive
        {
            get => gameObject.activeSelf;
            set
            {
                if (IsActive != value)
                {
                    gameObject.SetActive(value);
                }
            }
        }
    }
}