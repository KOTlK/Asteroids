namespace Game.Runtime.View
{
    public interface IVisualization<in T>
    {
        void Visualize(T view);
    }
}