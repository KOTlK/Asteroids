using Game.Runtime.Enemies;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class AsteroidViewFactory : MonoBehaviour, IAsteroidViewFactory
    {
        [SerializeField] private AsteroidView[] _asteroidsViews;

        public AsteroidView Create(Vector3 startPosition)
        {
            var random = Random.Range(0, _asteroidsViews.Length - 1);
            return Instantiate(_asteroidsViews[random], startPosition, Quaternion.identity);
        }
    }
}