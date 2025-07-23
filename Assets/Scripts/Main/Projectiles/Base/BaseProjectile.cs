using UnityEngine;

internal abstract class BaseProjectile : MonoBehaviour, IPoolableObjects {
    protected bool _isOccupied = false;
    public bool GetIsOccupied() => _isOccupied;
}