using Game.Runtime.Factories.View.Explosions;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public class BulletViewFactory : MonoBehaviour, IBulletViewFactory
    {
        [SerializeField] private BulletView _viewPrefab;
        [SerializeField] private ExplosionsFactory _explosionsFactory;
        
        public BulletView Create(Vector3 startPosition)
        {
            var view = Instantiate(_viewPrefab, startPosition, Quaternion.identity);
            view.Init(_explosionsFactory);

            return view;
        }
    }
}