using Game.Runtime.Enemies;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class EnemyShipViewFactory : MonoBehaviour, IEnemyShipViewFactory
    {
        [SerializeField] private EnemyShipView[] _prefabs;
        
        public EnemyShipView Create(Vector3 position)
        {
            return Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], position, Quaternion.Euler(0, 0, 180));
        }
    }
}