using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerAttack : MonoBehaviour {
    private enum e_attackType : int {
        normal = 0,
        special = 1,
    }

    [ReadOnly, SerializeField] private AnimationStateController _animationStateController = default;
    private InputAction _normalAttack = default;
    private InputAction _specialAttack = default;
    //REVIEW: Directionは本当にキャッシュするべきか？使わなければ消す
    [ReadOnly, SerializeField] private PlayerDirection _direction = default;
    [ReadOnly, SerializeField] private PlayerStamina _stamina = default;

    private PlayerAttackFactor _attackFactor = default;
    public float GetAttackFactor => _attackFactor.GetAttackFactor;

    public void Initialization(in PlayerInputs inputs, in AnimationStateController animationController, in PlayerDirection direction, in MonsterHandler handler, in PlayerStamina stamina) {
        _attackFactor = new PlayerAttackFactor();
        _animationStateController = animationController;
        _direction = direction;
        _stamina = stamina;

        _normalAttack = inputs[PlayerInputs.e_inputActions.normalAttack];
        _normalAttack.started += NormalAttack;
        _normalAttack.Enable();

        _specialAttack = inputs[PlayerInputs.e_inputActions.specialAttack];
        _specialAttack.started += SpecialAttack;
        _specialAttack.Enable();

        for (int i = 0; i < (int)PlayerGeneration.e_generation.max; i++) {
            handler[i].SetAttack(this.transform, _direction, _attackFactor, _stamina);
        }
    }

    private void OnDestroy() {
        _normalAttack.started -= NormalAttack;
        _normalAttack.Disable();
        _specialAttack.started -= SpecialAttack;
        _specialAttack.Disable();
    }

    private void NormalAttack(InputAction.CallbackContext context) {
        if (_stamina.IsRunOutOfStamina) {
            return;
        }
        _animationStateController.SetAttack((int)e_attackType.normal);
    }

    private void SpecialAttack(InputAction.CallbackContext context) {
        if (_stamina.IsRunOutOfStamina) {
            return;
        }
        _animationStateController.SetAttack((int)e_attackType.special);
    }
}