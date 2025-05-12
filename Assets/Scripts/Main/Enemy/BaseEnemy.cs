using System;
using UnityEngine;

internal class BaseEnemy : MonoBehaviour {
    public static event Action<int> OnDefeated;

    [SerializeField] private int _baseExp = 10;
    [SerializeField] protected int _hp = 20;

    protected void Defeated() {
        OnDefeated?.Invoke(_baseExp);
    }
}