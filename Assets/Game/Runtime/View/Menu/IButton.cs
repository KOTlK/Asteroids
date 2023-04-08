namespace Game.Runtime.View.Menu
{
    public interface IButton : IElement
    {
        bool Clicked { get; }
        void Release();
    }
}