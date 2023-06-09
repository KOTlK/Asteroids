﻿using Game.Runtime.View.Menu;
using Game.Runtime.View.Ship;
using Game.Runtime.View.Viewport;

namespace Game.Runtime.View
{
    public interface IViewRoot
    {
        IShipInterface ShipInterface { get; }
        IMainMenu MainMenu { get; }
        ILoseScreen LoseScreen { get; }
        IInGameView InGameView { get; }
        IViewport Viewport { get; }
    }
}