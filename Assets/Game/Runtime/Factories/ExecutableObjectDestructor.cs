using System;
using System.Collections.Generic;
using Game.Runtime.GameLoop;

namespace Game.Runtime.Factories
{
    public class ExecutableObjectDestructor<T> : IObjectDestructor<T>, ILoop
    where T : IDisposable, ILoop
    {
        private readonly List<T> _spawned = new();
        private readonly Queue<T> _destroyQueue = new();

        public ExecutableObjectDestructor()
        {
        }

        public void Add(T obj)
        {
            _spawned.Add(obj);
        }

        public void Destroy(T obj)
        {
            _destroyQueue.Enqueue(obj);
        }

        public void Execute(float deltaTime)
        {
            while (_destroyQueue.Count > 0)
            {
                var obj = _destroyQueue.Dequeue();
                _spawned.Remove(obj);
                obj.Dispose();
            }

            foreach (var obj in _spawned)
            {
                obj.Execute(deltaTime);
            }
        }

        public void Dispose()
        {
            foreach (var obj in _spawned)
            {
                obj.Dispose();
            }
        }
    }
}