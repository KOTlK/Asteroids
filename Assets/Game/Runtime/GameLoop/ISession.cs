using System;
using Game.Runtime.Factories;

namespace Game.Runtime.GameLoop
{
    public interface ISession : ILoop, IDisposable
    {
        bool GameLose { get; }
        void Start(ShipReference selectedShip);
    }
}