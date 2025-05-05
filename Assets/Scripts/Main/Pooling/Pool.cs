using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
internal sealed class Pool<T> where T : MonoBehaviour, IPoolableObjects {

    private T _prefab = default;
    private Queue<T> _pool = default;

    public Pool() { }

    public void MakePool(in T prefab, int poolSize) {
        _pool = new Queue<T>(poolSize);
        _prefab = prefab;
        for (int i = 0; i < poolSize; i++) {
            T instance = UnityEngine.Object.Instantiate(_prefab);
            _pool.Enqueue(instance);
        }
    }
    public void MakePool(in T prefab, int poolSize, in Transform parent) {
        _pool = new Queue<T>(poolSize);
        _prefab = prefab;
        for (int i = 0; i < poolSize; i++) {
            T instance = UnityEngine.Object.Instantiate(_prefab);
            instance.transform.SetParent(parent, false);
            _pool.Enqueue(instance);
        }
    }
    public void ResetPool() {
        _pool.Clear();
    }
    public T GetPooledObject() {
        if (_pool.Peek().GetIsOccupied()) {
            T generatedInstance = UnityEngine.Object.Instantiate(_prefab);
            _pool.Enqueue(generatedInstance);
            return generatedInstance;
        }
        T instance = _pool.Dequeue();
        _pool.Enqueue(instance);
        return instance;
    }
    public T GetPooledObject(in Transform parent) {
        if (_pool.Peek().GetIsOccupied()) {
            T generatedInstance = UnityEngine.Object.Instantiate(_prefab);
            generatedInstance.transform.SetParent(parent, false);
            _pool.Enqueue(generatedInstance);
            return generatedInstance;
        }
        T instance = _pool.Dequeue();
        _pool.Enqueue(instance);
        return instance;
    }
}