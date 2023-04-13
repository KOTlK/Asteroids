using Game.Runtime.View;
using Game.Runtime.View.Menu;

namespace Game.Runtime.GameLoop
{
    public class MainLoop : ILoop
    {
        private readonly ISession _session;
        private readonly IViewRoot _viewRoot;

        private ILoop _executingLoop;

        public MainLoop(ISession session, IViewRoot viewRoot)
        {
            _session = session;
            _viewRoot = viewRoot;
            _executingLoop = viewRoot.MainMenu;
        }

        public void Execute(float deltaTime)
        {
            switch (_executingLoop)
            {
                case IMainMenu mainMenu:
                    if (mainMenu.StartGameButton.Clicked)
                    {
                        mainMenu.StartGameButton.Release();
                        _session.Start(mainMenu.ShipPicker.Selected.Type);
                        _viewRoot.InGameView.IsActive = true;
                        mainMenu.IsActive = false;
                        _executingLoop = _session;
                        return;
                    }

                    if (mainMenu.ExitButton.Clicked)
                    {
                        mainMenu.ExitButton.Release();
                        UnityEngine.Application.Quit();
                        return;
                    }

                    break;
                
                case ISession session:
                    if (session.GameLose)
                    {
                        _viewRoot.LoseScreen.IsActive = true;
                        _viewRoot.InGameView.IsActive = false;
                        _executingLoop = _viewRoot.LoseScreen;
                        return;
                    }

                    break;
                    
                case ILoseScreen loseScreen:
                    _session.Visualize(loseScreen.Score);
                    if (loseScreen.Restart.Clicked)
                    {
                        loseScreen.Restart.Release();
                        _session.Restart();
                        loseScreen.IsActive = false;
                        _viewRoot.InGameView.IsActive = true;
                        _executingLoop = _session;
                    }
                    
                    if (loseScreen.ExitGame.Clicked)
                    {
                        loseScreen.ExitGame.Release();
                        _session.Dispose();
                        UnityEngine.Application.Quit();
                        return;
                    }

                    if (loseScreen.ExitToMenu.Clicked)
                    {
                        loseScreen.ExitToMenu.Release();
                        _viewRoot.LoseScreen.IsActive = false;
                        _viewRoot.MainMenu.IsActive = true;
                        _session.Dispose();
                        _executingLoop = _viewRoot.MainMenu;
                        return;
                    }
                    break;
            }
            
            _executingLoop.Execute(deltaTime);
        }
    }
}