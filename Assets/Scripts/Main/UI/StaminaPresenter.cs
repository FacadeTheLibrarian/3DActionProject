using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

internal sealed class StaminaPresenter : MonoBehaviour {
    [SerializeField] private PlayerStamina _playerStamina = default;
    [SerializeField] private StaminaView _staminaModel = default; 

    //private void Awake() {
    //    _playerStamina.Stamina.Subscribe(
    //        (x) => {
    //            _staminaModel.OnStaminaChanged(x / _playerStamina.GetStaminaMax);
    //        }
    //    ).AddTo(this);
    //}
}