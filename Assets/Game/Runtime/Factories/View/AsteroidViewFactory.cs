using Game.Runtime.Enemies;
using Game.Runtime.Factories.View.Explosions;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class AsteroidViewFactory : MonoBehaviour, IAsteroidViewFactory
    {
        [SerializeField] private AsteroidView[] _asteroidsViews;
        [SerializeField] private ExplosionsFactory _explosionsFactory;

        public AsteroidView Create(Vector3 startPosition)
        {
            var random = Random.Range(0, _asteroidsViews.Length - 1);
            var view = Instantiate(_asteroidsViews[random], startPosition, Quaternion.identity);
            view.Init(_explosionsFactory);
            return view;
        }
    }
}