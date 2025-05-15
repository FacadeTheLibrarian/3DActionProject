using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerAttack : MonoBehaviour {
    [SerializeField] private Animator _animator = default;
    [SerializeField] private InputActionReference _attackMove = default;
    [SerializeField] private InputActionReference _specialMove = default;

    private void Awake() {
        
    }

    private void NormalAttack(InputAction.CallbackContext context) {
        //_animator[(int)_currentGeneration].SetTrigger("Attack");
        //_animator[(int)_currentGeneration].SetInteger("AttackType", 0);
    }

    private void SpecialAttack(InputAction.CallbackContext context) {
        //_animator[(int)_currentGeneration].SetTrigger("Attack");
        //_animator[(int)_currentGeneration].SetInteger("AttackType", 1);
    }
}