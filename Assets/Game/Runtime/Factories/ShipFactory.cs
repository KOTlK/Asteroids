using System;
using System.Collections.Generic;
using Game.Runtime.Factories.View;
using Game.Runtime.Input.Ship;
using Game.Runtime.Physics;
using Game.Runtime.Ship;
using Game.Runtime.Ship.Hp;
using Game.Runtime.Ship.Weapons;
using Game.Runtime.View;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public class ShipFactory : IShipFactory
    {
        private readonly IBulletsFactory _bulletsFactory;
        private readonly ICollidersWorld<IDamageable> _collidersWorld;
        private readonly IPlayerShipViewFactory _viewFactory;
        private readonly IViewRoot _viewRoot;
        private readonly Dictionary<ShipType, ShipReference> _shipReferences;
        private readonly ExecutableObjectDestructor<Ship.Ship> _destructor = new();

        public ShipFactory(IBulletsFactory bulletsFactory, ICollidersWorld<IDamageable> collidersWorld, IPlayerShipViewFactory viewFactory, IViewRoot viewRoot, Dictionary<ShipType, ShipReference> shipReferences)
        {
            _bulletsFactory = bulletsFactory;
            _collidersWorld = collidersWorld;
            _viewFactory = viewFactory;
            _viewRoot = viewRoot;
            _shipReferences = shipReferences;
        }

        public Ship.Ship Create(ShipType type, Vector3 position, IShipInput input)
        {
            var reference = _shipReferences[type];
            var view = _viewFactory.Create(type);
            var collider = new AABBCollider(new AABB()
            {
                Size = new Vector3(1, 1),
                Center = position,
            });
            
            var model =  new Ship.Ship(
                new ShipVisualization(
                    view,
                    _viewRoot.ShipInterface),
                collider,
                new Health(reference.Stats.MaxHealth),
                input,
                reference.Stats,
                _viewRoot.Viewport,
                new StandardWeapon(
                    Vector3.up,
                    _bulletsFactory,
                    view,
                    reference.Stats.WeaponStats));

            _collidersWorld.Add(collider, model);
            _destructor.Add(model);

            return model;
        }

        public void Destroy(Ship.Ship model)
        {
            _destructor.Destroy(model);
            _collidersWorld.Remove(model);
        }

        public void Execute(float deltaTime) => _destructor.Execute(deltaTime);
    }

    [Serializable]
    public struct ShipReference
    {
        public string Name;
        public ShipType Type;
        public ShipStats Stats;
    }
}