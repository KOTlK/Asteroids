﻿using Game.Runtime.Enemies;
using Game.Runtime.GameLoop;
using UnityEngine;

namespace Game.Runtime.Factories
{
    public interface IAsteroidsFactory : IObjectDestroyer<Asteroid>, ILoop
    {
        Asteroid Create(float speed, float damage, Vector3 startPosition);
    }
}