using System.Collections.Generic;
using UnityEngine;

internal class ProjectilePooling<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObjects {

    private T _prefab = default;
    private Queue<T> _pool = default;

    public void MakePool(T prefab, int poolSize) {
        _pool = new Queue<T>(poolSize);
        _prefab = prefab;
        for (int i = 0; i < poolSize; i++) {
            T instance = Instantiate(_prefab);
            instance.transform.SetParent(this.transform, false);
            _pool.Enqueue(instance);
        }
    }
    public void ResetPool() {
        _pool.Clear();
    }
    public T GetPooledObject() {
        if (_pool.Peek().GetIsOccupied()) {
            T generatedInstance = Instantiate(_prefab);
             generatedInstance.transform.SetParent(this.transform, false);
            _pool.Enqueue(generatedInstance);
            return generatedInstance;
        }
        T instance = _pool.Dequeue();
        _pool.Enqueue(instance);
        return instance;
    }
}