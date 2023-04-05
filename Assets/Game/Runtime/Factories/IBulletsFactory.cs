using Game.Runtime.GameLoop;
using Game.Runtime.Physics;
using Game.Runtime.Ship.Weapons;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public interface IBulletsFactory : IObjectDestructor<IBullet>, ILoop
    {
        IBullet Create(Vector3 startPosition, float damage, float speed);
    }
}