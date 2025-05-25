using System;
using UnityEngine;

internal class BaseEnemy : MonoBehaviour {
    public static event Action<float> OnDefeated;

    [SerializeField] private float _baseExp = 10;
    [SerializeField] protected int _hp = 20;

    protected void Defeated() {
        OnDefeated?.Invoke(_baseExp);
    }
}