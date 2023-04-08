using Game.Runtime.Factories;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Application
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Factories.Factories _factories;
        [SerializeField] private ShipInput _shipInput;
        [SerializeField] private ViewRoot _viewRoot;
        [SerializeField] private ShipReference[] _shipsSettings;

        private ILoop _game;

        private void Start()
        {
            _game = new MainLoop(
                new Session(
                    _factories,
                    _viewRoot,
                    _shipInput),
                _viewRoot);
        }

        private void Update()
        {
            _game.Execute(Time.deltaTime);
        }
    }
}