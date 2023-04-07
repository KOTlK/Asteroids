using Game.Runtime.GameLoop;

namespace Game.Runtime.Enemies
{
    public interface IKamikaze : ILoop
    {
        bool Destroyed { get; }
    }
}