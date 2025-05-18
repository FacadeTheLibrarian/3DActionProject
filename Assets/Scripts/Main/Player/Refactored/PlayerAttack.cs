using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerAttack : MonoBehaviour {
    [SerializeField] private AnimationStateController _animator = default;
    [SerializeField] private InputAction _normalAttack = default;
    [SerializeField] private InputAction _specialAttack = default;
    [SerializeField] private PlayerDirection _direction = default;

    private PlayerAttackFactor _attackFactor = default;

    public void Initialization(in PlayerInputs inputs, in AnimationStateController animator, in PlayerDirection direction, in MonsterHandler handler) {
        _attackFactor = new PlayerAttackFactor();
        _animator = animator;
        _direction = direction;

        _normalAttack = inputs[PlayerInputs.e_inputActions.normalAttack];
        _normalAttack.started += NormalAttack;
        _normalAttack.Enable();

        _specialAttack = inputs[PlayerInputs.e_inputActions.specialAttack];
        _specialAttack.started += SpecialAttack;
        _specialAttack.Enable();

        for (int i = 0; i < (int)MonsterHandler.e_generation.max; i++) {
            handler[i].SetAttack(this.transform, _direction, _attackFactor);
        }
    }

    private void OnDestroy() {
        _normalAttack.started -= NormalAttack;
        _normalAttack.Disable();
        _specialAttack.started -= SpecialAttack;
        _specialAttack.Disable();
    }

    private void NormalAttack(InputAction.CallbackContext context) {

        _animator.SetAttack(0);
    }

    private void SpecialAttack(InputAction.CallbackContext context) {
        _animator.SetAttack(1);
    }
}