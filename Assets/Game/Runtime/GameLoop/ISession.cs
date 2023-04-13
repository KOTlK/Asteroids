using System;
using Game.Runtime.Factories;
using Game.Runtime.View;
using Game.Runtime.View.Score;

namespace Game.Runtime.GameLoop
{
    public interface ISession : ILoop, IDisposable, IVisualization<IScoreView>
    {
        bool GameLose { get; }
        void Start(ShipReference selectedShip);
        void Restart();
    }
}