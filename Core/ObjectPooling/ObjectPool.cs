using UnityEngine;
using System;
using System.Collections.Generic;



namespace DenizYanar.Pool
{
    public class ObjectPool<T>
    {
        private readonly Queue<T> _availableObjects = new Queue<T>();

        private readonly Func<T> _spawn;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;
        private readonly Action<T> _onInit;

        public Transform RootObject { get; }

        public ObjectPool(Func<T> spawn, Action<T> onGet = null, Action<T> onRelease = null, Action<T> onInit = null)
        {
            _spawn = spawn;
            _onGet = onGet;
            _onRelease = onRelease;
            _onInit = onInit;

            RootObject = new GameObject("Sound Emitter Pool").transform;
        }


        public T Get()
        {
            T obj;

            if (_availableObjects.Count > 0)
            {
                obj = _availableObjects.Dequeue();
                _onGet?.Invoke(obj);
                return obj;
            }

            obj = _spawn();
            _onInit?.Invoke(obj);
            _onGet?.Invoke(obj);
            return obj;

        }

        public void Release(T obj)
        {
            _availableObjects.Enqueue(obj);
            _onRelease?.Invoke(obj);
        }

        
    }  
}
