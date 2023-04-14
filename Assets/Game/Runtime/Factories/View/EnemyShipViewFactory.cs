using Game.Runtime.Enemies;
using Game.Runtime.Factories.View.Explosions;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class EnemyShipViewFactory : MonoBehaviour, IEnemyShipViewFactory
    {
        [SerializeField] private EnemyShipView[] _prefabs;
        [SerializeField] private ExplosionsFactory _explosionsFactory;
        
        public EnemyShipView Create(Vector3 position)
        {
            var view = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], position, Quaternion.Euler(0, 0, 180));
            view.Init(_explosionsFactory.Create());
            return view;
        }
    }
}