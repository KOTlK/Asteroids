using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public interface IBulletViewFactory
    {
        BulletView Create(Vector3 startPosition);
    }
}