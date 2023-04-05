using Game.Runtime.Enemies;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public interface IEnemyShipViewFactory
    {
        EnemyShipView Create(Vector3 position);
    }
}