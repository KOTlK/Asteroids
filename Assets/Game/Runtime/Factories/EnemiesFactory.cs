using System.Collections.Generic;
using Game.Runtime.Enemies;
using Game.Runtime.Factories.View;
using Game.Runtime.GameLoop;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class EnemiesFactory : IObjectDestructor<EnemyShip>, ILoop
    {
        private readonly IEnemyShipViewFactory _viewFactory;
        private readonly IBulletsFactory _bulletsFactory;
        private readonly ICollidersWorld<IDamageable> _collidersWorld;
        private readonly IColliderCaster<IDamageable> _targetColliders;
        private readonly ExecutableObjectDestructor<EnemyShip> _destructor = new();

        public EnemiesFactory(IEnemyShipViewFactory viewFactory, IBulletsFactory bulletsFactory, ICollidersWorld<IDamageable> collidersWorld, IColliderCaster<IDamageable> targetColliders)
        {
            _viewFactory = viewFactory;
            _bulletsFactory = bulletsFactory;
            _collidersWorld = collidersWorld;
            _targetColliders = targetColliders;
        }

        //temp
        private readonly List<EnemyShipInput> _inputs = new();

        public EnemyShip Create(Vector3 position)
        {
            var view = _viewFactory.Create(position);
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
                _targetColliders,
                view.Stats);

            _inputs.Add(input);
            _collidersWorld.Add(collider, model);
            _destructor.Add(model);
            
            return model;
        }

        public void Destroy(EnemyShip obj)
        {
            _collidersWorld.Remove(obj);
            _destructor.Destroy(obj);
        }

        public void Execute(float deltaTime)
        {
            foreach (var input in _inputs)
            {
                input.Execute(deltaTime);
            }
            _destructor.Execute(deltaTime);
        }
    }
}