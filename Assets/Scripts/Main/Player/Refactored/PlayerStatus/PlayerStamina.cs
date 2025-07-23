using System;
using UniRx;
using UnityEngine;

internal sealed class PlayerStamina : MonoBehaviour {
    private float _baseStaminaMax = 0.0f;
    private float _baseStaminaRecoveryAmountPerSecond = 0.0f;

    public event Action<float> OnStaminaChanged = default;
    private float _stamina = 0.0f;

    [SerializeField] private AnimationStateController _animationStateController = default;
    [SerializeField] private bool _isRunOutOfStamina = false;
    [SerializeField] private float _staminaMax = 0.0f;
    [SerializeField] private float _staminaRecoveryAmount = 0.0f;
    [SerializeField] private float _staminaRecoveryFactor = 1.0f;

    public float GetStamina => _stamina;
    public bool IsRunOutOfStamina => _isRunOutOfStamina;

    public void Initialization(in AnimationStateController animationStateController, in MonsterData monsterData) {
        _animationStateController = animationStateController;
        _baseStaminaMax = monsterData.GetInitialStamina;
        _baseStaminaRecoveryAmountPerSecond = monsterData.GetStaminaRecoveryAmountPerSecond;
        _staminaMax = _baseStaminaMax;
        _stamina = _baseStaminaMax;
        _staminaRecoveryAmount = _baseStaminaRecoveryAmountPerSecond;
    }
    public void UseStamina(float amount) {
        float nextStamina = _stamina - amount;
        if (nextStamina <= 0.0f) {
            _isRunOutOfStamina = true;
            nextStamina = 0.0f;
        }
        _stamina = nextStamina;
        OnStaminaChanged?.Invoke(_stamina / _staminaMax);
        return;
    }
    public void UpdateStamina() {
        if (_animationStateController.IsMoveInvalid) {
            return;
        }
        if (_stamina >= _staminaMax) {
            return;
        }
        float nextRecoveryAmount = _staminaRecoveryAmount * _staminaRecoveryFactor * Time.deltaTime;
        float nextStamina = _stamina + nextRecoveryAmount;
        if (nextStamina >= _staminaMax) {
            _isRunOutOfStamina = false;
            nextStamina = _staminaMax;
        }
        _stamina = nextStamina;
        OnStaminaChanged?.Invoke(_stamina / _staminaMax);
    }
}