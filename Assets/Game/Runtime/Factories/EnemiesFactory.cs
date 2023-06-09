﻿using System.Collections.Generic;
using Game.Runtime.Enemies;
using Game.Runtime.Factories.View;
using Game.Runtime.GameLoop.Score;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Movement;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View.Viewport;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Runtime.Factories
{
    public class EnemiesFactory : IEnemiesFactory
    {
        private readonly IEnemyShipViewFactory _viewFactory;
        private readonly IBulletsFactory _bulletsFactory;
        private readonly ICollidersWorld<IDamageable> _collidersWorld;
        private readonly IColliderCaster<IDamageable> _targetColliders;
        private readonly IViewport _viewport;
        private readonly IScore _score;
        private readonly ExecutableObjectDestructor<EnemyShip> _destructor = new();

        public EnemiesFactory(IEnemyShipViewFactory viewFactory,
            IBulletsFactory bulletsFactory,
            ICollidersWorld<IDamageable> collidersWorld,
            IColliderCaster<IDamageable> targetColliders,
            IViewport viewport,
            IScore score)
        {
            _viewFactory = viewFactory;
            _bulletsFactory = bulletsFactory;
            _collidersWorld = collidersWorld;
            _targetColliders = targetColliders;
            _viewport = viewport;
            _score = score;
        }

        //temp
        private readonly List<EnemyShipInput> _inputs = new();

        public int ActiveCount { get; private set; }

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
                    new Body<IDamageable>(
                        collider,
                        _targetColliders,
                        position),
                    view.Stats.DamageOnCollision),
                _score);

            _inputs.Add(input);
            _collidersWorld.Add(collider, model);
            _destructor.Add(model);
            ActiveCount++;
            return model;
        }

        public void Destroy(EnemyShip obj)
        {
            _collidersWorld.Remove(obj);
            _destructor.Destroy(obj);
            ActiveCount--;
        }

        public void Execute(float deltaTime)
        {
            foreach (var input in _inputs)
            {
                input.Execute(deltaTime);
            }
            _destructor.Execute(deltaTime);
        }

        public void Dispose()
        {
            _destructor.Dispose();
        }
    }
}