using System;
using UnityEngine;

internal sealed class PlayerHitPoint : MonoBehaviour {
    private const float HP_BASE_MAX = 100;

    public event Action<float> OnHitPointChanged = default;
    private float _hitPointMax = HP_BASE_MAX;
    private float _hitPoint = HP_BASE_MAX;

    public float GetHitPoint => _hitPoint;

#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _hitPoint -= 10;
            OnHitPointChanged?.Invoke(_hitPoint / _hitPointMax);
            Debug.Log(_hitPoint / _hitPointMax);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _hitPoint += 10;
            OnHitPointChanged?.Invoke(_hitPoint / _hitPointMax);
            Debug.Log(_hitPoint / _hitPointMax);
        }
    }
#endif
}