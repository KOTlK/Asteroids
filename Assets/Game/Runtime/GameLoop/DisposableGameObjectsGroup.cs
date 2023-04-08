using System;

namespace Game.Runtime.GameLoop
{
    public class DisposableGameObjectsGroup : IDisposableLoop
    {
        private readonly ILoop[] _gameObjects;

        public DisposableGameObjectsGroup(ILoop[] gameObjects)
        {
            _gameObjects = gameObjects;
        }

        public void Execute(float deltaTime)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Execute(deltaTime);
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _gameObjects.Length; i++)
            {
                var go = _gameObjects[i];

                if (go is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _gameObjects[i] = null;
            }
        }
    }
}