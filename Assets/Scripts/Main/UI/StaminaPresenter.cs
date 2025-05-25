using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

internal sealed class StaminaPresenter : MonoBehaviour {
    [SerializeField] private PlayerStamina _model = default;
    [SerializeField] private StaminaView _view = default;

    private void Awake() {
        _model.OnStaminaChanged += _view.OnStaminaChanged;
    }

    private void OnDestroy() {
        _model.OnStaminaChanged -= _view.OnStaminaChanged;
    }
}