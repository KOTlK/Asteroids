using Game.Runtime.Input.Ship;
using Game.Runtime.Ship;
using UnityEngine;

namespace Game.Runtime.Application
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Factories.Factories _factories;
        [SerializeField] private ShipInput _shipInput;

        private ShipModel _shipModel;

        private void Awake()
        {
            _shipModel = _factories.ShipFactory.Create(ShipType.Fast, _shipInput);
        }

        private void Update()
        {
            _shipModel.Execute(Time.deltaTime);
        }
    }
}