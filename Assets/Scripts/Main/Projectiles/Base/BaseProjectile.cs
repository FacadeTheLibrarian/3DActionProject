using System.Collections.Generic;
using UnityEngine;

internal class BaseProjectile : MonoBehaviour, IPoolableObjects {
    protected bool _isOccupied = false;
    public bool GetIsOccupied() => _isOccupied;
}