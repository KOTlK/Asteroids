using Game.Runtime.Enemies;
using UnityEngine;

namespace Game.Runtime.Factories.View
{
    public interface IAsteroidViewFactory
    {
        AsteroidView Create(Vector3 startPosition);
    }
}