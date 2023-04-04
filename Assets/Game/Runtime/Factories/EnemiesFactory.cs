using System.Collections.Generic;
using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class EnemiesFactory : MonoBehaviour, IObjectDestroyer<EnemyShip>, ILoop
    {
        [SerializeField] private EnemyShipView[] _prefabs;
        [SerializeField] private BulletsFactory _bulletsFactory;

        private readonly ExecutableObjectDestroyer<EnemyShip> _destroyer = new();

        //temp
        private readonly List<EnemyShipInput> _inputs = new();

        public EnemyShip Create(Vector3 position, ICollidersWorld<IDamageable> collidersWorld, IColliderCaster<IDamageable> colliderCaster)
        {
            var view = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], position, Quaternion.Euler(0, 0, 180));
            var collider = new AABBCollider(new AABB()
            {
                Center = position,
                Size = new Vector3(1, 1)
            });
            var input = new EnemyShipInput(2f);
            var model = new EnemyShip(
                position,
                view,
                this,
                _bulletsFactory,
                collider,
                input,
                colliderCaster,
                view.Stats);

            _inputs.Add(input);
            collidersWorld.Add(collider, model);
            _destroyer.Add(model);
            
            return model;
        }

        public void Destroy(EnemyShip obj) => _destroyer.Destroy(obj);

        public void Execute(float deltaTime)
        {
            foreach (var input in _inputs)
            {
                input.Execute(deltaTime);
            }
            _destroyer.Execute(deltaTime);
        }
    }
}