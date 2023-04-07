using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public interface IEnemiesFactory : IObjectDestructor<EnemyShip>, ILoop
    {
        EnemyShip Create(Vector3 position);
    }
}