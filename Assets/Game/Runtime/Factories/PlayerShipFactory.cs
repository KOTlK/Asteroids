using System;
using System.Collections.Generic;
using Game.Runtime.Factories.View;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Movement;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class PlayerShipFactory : IPlayerShipFactory
    {
        private readonly IBulletsFactory _bulletsFactory;
        private readonly ICollidersWorld<IDamageable> _collidersWorld;
        private readonly IPlayerShipViewFactory _viewFactory;
        private readonly IViewRoot _viewRoot;
        private readonly ExecutableObjectDestructor<PlayerShip> _destructor = new();

        public PlayerShipFactory(IBulletsFactory bulletsFactory, ICollidersWorld<IDamageable> collidersWorld, IPlayerShipViewFactory viewFactory, IViewRoot viewRoot)
        {
            _bulletsFactory = bulletsFactory;
            _collidersWorld = collidersWorld;
            _viewFactory = viewFactory;
            _viewRoot = viewRoot;
        }

        public PlayerShip Create(ShipReference reference, Vector3 position, IShipInput input)
        {
            var view = _viewFactory.Create(reference.Type);
            var collider = new AABBCollider(new AABB()
            {
                Size = new Vector3(1, 1),
                Center = position,
            });

            var model = new PlayerShip(
                new Ship.Ship(
                    new Health(reference.Stats.MaxHealth),
                    new PlayerShipMovement(
                        input,
                        reference.Stats,
                        _viewRoot.Viewport,
                        view,
                        collider,
                        position),
                    input,
                    new StandardWeapon(
                        Vector3.up,
                        _bulletsFactory,
                        view,
                        reference.Stats.WeaponStats)),
                new ShipVisualization(
                    view,
                    _viewRoot.ShipInterface));

            _collidersWorld.Add(collider, model);
            _destructor.Add(model);

            return model;
        }

        public void Destroy(PlayerShip model)
        {
            _destructor.Destroy(model);
            _collidersWorld.Remove(model);
        }

        public void Execute(float deltaTime) => _destructor.Execute(deltaTime);

        public void Dispose()
        {
            _destructor.Dispose();
        }
    }

    [Serializable]
    public struct ShipReference
    {
        public string Name;
        public ShipType Type;
        public ShipStats Stats;
    }
}