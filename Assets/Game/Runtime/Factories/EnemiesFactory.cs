using System.Collections.Generic;
using Game.Runtime.Enemies;
using Game.Runtime.Factories.View;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Movement;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class EnemiesFactory : IEnemiesFactory
    {
        private readonly IEnemyShipViewFactory _viewFactory;
        private readonly IBulletsFactory _bulletsFactory;
        private readonly ICollidersWorld<IDamageable> _collidersWorld;
        private readonly IColliderCaster<IDamageable> _targetColliders;
        private readonly IViewport _viewport;
        private readonly ExecutableObjectDestructor<EnemyShip> _destructor = new();

        public EnemiesFactory(IEnemyShipViewFactory viewFactory, IBulletsFactory bulletsFactory, ICollidersWorld<IDamageable> collidersWorld, IColliderCaster<IDamageable> targetColliders, IViewport viewport)
        {
            _viewFactory = viewFactory;
            _bulletsFactory = bulletsFactory;
            _collidersWorld = collidersWorld;
            _targetColliders = targetColliders;
            _viewport = viewport;
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
            var direction = Random.insideUnitCircle.normalized;
            var input = new EnemyShipInput(2f, 0.5f, direction);
            
            var model = new EnemyShip(
                new Ship.Ship(
                    new Health(view.Stats.MaxHealth),
                    new EnemyShipMovement(
                        view.Stats.Speed,
                        input,
                        _viewport,
                        view,
                        collider,
                        position),
                    input,
                    new StandardWeapon(
                        Vector3.down,
                        _bulletsFactory,
                        view,
                        view.Stats.WeaponStats)),
                view,
                this,
                new Kamikaze(
                    collider,
                    _targetColliders,
                    view.Stats.DamageOnCollision));

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