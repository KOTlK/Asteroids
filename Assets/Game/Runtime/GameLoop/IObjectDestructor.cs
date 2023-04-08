using System;

namespace Game.Runtime.GameLoop
{
    public interface IObjectDestructor<in TObject> : IDisposable
    {
        public void Destroy(TObject obj);
    }
}