namespace Game.Runtime.GameLoop
{
    public interface IObjectDestructor<in TObject>
    {
        public void Destroy(TObject obj);
    }
}