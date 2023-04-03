namespace Game.Runtime.GameLoop
{
    public interface IObjectDestroyer<in TObject>
    {
        public void Destroy(TObject obj);
    }
}