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
                        _executingLoop = _session;
                        mainMenu.IsActive = false;
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
                        session.Dispose();
                        _executingLoop = _viewRoot.MainMenu;
                        _viewRoot.MainMenu.IsActive = true;
                        return;
                    }

                    break;
                    
            }
            
            _executingLoop.Execute(deltaTime);
        }
    }
}