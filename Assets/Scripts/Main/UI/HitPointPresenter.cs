using System.Collections.Generic;
using UnityEngine;

internal sealed class HitPointPresenter : MonoBehaviour {
    [SerializeField] private PlayerHitPoint _playerHitPoint = default;
    [SerializeField] private HitPointView _hitPointView = default;

    private void Awake() {
        _playerHitPoint.OnHitPointChanged += _hitPointView.OnHitPointChanged;
    }

    private void OnDestroy() {
        _playerHitPoint.OnHitPointChanged -= _hitPointView.OnHitPointChanged;
    }
}