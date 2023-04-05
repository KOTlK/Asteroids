using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class BulletViewFactory : MonoBehaviour, IBulletViewFactory
    {
        [SerializeField] private BulletView _viewPrefab;
        
        public BulletView Create(Vector3 startPosition)
        {
            return Instantiate(_viewPrefab, startPosition, Quaternion.identity);
        }
    }
}