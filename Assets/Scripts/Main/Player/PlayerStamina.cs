using UniRx;
using UnityEngine;

internal sealed class PlayerStamina : MonoBehaviour {
    private const float STAMINA_BASE_MAX = 100.0f;
    private const float STAMINA_BASE_RECOVERY_AMOUNT = 20.0f;

    [SerializeField] private bool _isRunOutOfStamina = false;
    [SerializeField] private float _staminaMax = STAMINA_BASE_MAX;
    [SerializeField] private float _staminaRecoveryAmount = STAMINA_BASE_RECOVERY_AMOUNT;

    private readonly FloatReactiveProperty STAMINA = new FloatReactiveProperty(STAMINA_BASE_MAX);
    public IReadOnlyReactiveProperty<float> Stamina => STAMINA;
    public float GetStaminaMax => _staminaMax;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            STAMINA.Value -= 10.0f;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            STAMINA.Value += 10.0f;
        }
    }

    private void OnDestroy() {
        STAMINA.Dispose();
    }

    public bool TryUseStamina(float amount) {
        if (_isRunOutOfStamina) {
            return false;
        }
        float currentStamina = STAMINA.Value;
        if (currentStamina - amount <= 0.0f) {
            _isRunOutOfStamina = true;
            STAMINA.Value = 0.0f;
            return true;
        }
        STAMINA.Value -= amount;
        return true;
    }
    //public void RecoverStamina() {
    //    if (_onDodge) {
    //        return;
    //    }
    //    if (_hasSprintInput) {
    //        return;
    //    }
    //    if (STAMINA >= _staminaMax) {
    //        return;
    //    }
    //    STAMINA += _staminaRecoveryAmount * Time.deltaTime;
    //    if (STAMINA >= _staminaMax) {
    //        _isRunOutOfStamina = false;
    //        STAMINA = _staminaMax;
    //    }
    //}
}