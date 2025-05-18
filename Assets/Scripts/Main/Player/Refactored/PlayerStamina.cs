using System;
using UniRx;
using UnityEngine;

internal sealed class PlayerStamina : MonoBehaviour {
    private const float STAMINA_BASE_MAX = 100.0f;
    private const float STAMINA_BASE_RECOVERY_AMOUNT = 20.0f;

    [SerializeField] private AnimationStateController _animationStateController = default;
    private readonly FloatReactiveProperty STAMINA = new FloatReactiveProperty(STAMINA_BASE_MAX);
    public IReadOnlyReactiveProperty<float> Stamina => STAMINA;

    [SerializeField] private bool _isRunOutOfStamina = false;
    [SerializeField] private float _staminaMax = STAMINA_BASE_MAX;
    [SerializeField] private float _staminaRecoveryAmount = STAMINA_BASE_RECOVERY_AMOUNT;
    [SerializeField] private float _staminaRecoveryFactor = 1.0f;

    public float GetStaminaMax => _staminaMax;
    public bool IsRunOutOfStamina => _isRunOutOfStamina;

    public void Initialization(in AnimationStateController animationStateController) {
        _animationStateController = animationStateController;
    }

#if UNITY_EDITOR
    private void Update() {
        Debug.Log(STAMINA.Value);
        if (Input.GetKeyDown(KeyCode.O)) {
            STAMINA.Value -= 10.0f;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            STAMINA.Value += 10.0f;
        }
    }
#endif

    private void OnDestroy() {
        STAMINA.Dispose();
    }

    public void UseStamina(float amount) {
        float currentStamina = STAMINA.Value;
        Debug.Log(amount);
        if (currentStamina - amount <= 0.0f) {
            _isRunOutOfStamina = true;
            STAMINA.Value = 0.0f;
            return;
        }
        STAMINA.Value -= amount;
        return;
    }
    public void UpdateStamina() {
        if (_animationStateController.IsMoveInvalid) {
            return;
        }
        if (STAMINA.Value >= _staminaMax) {
            return;
        }
        float currentStamina = STAMINA.Value;
        float nextRecoveryAmount = _staminaRecoveryAmount * _staminaRecoveryFactor * Time.deltaTime;
        if (currentStamina + nextRecoveryAmount >= _staminaMax) {
            _isRunOutOfStamina = false;
            STAMINA.Value = _staminaMax;
            return;
        }
        STAMINA.Value += nextRecoveryAmount;
    }
}