using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerInputs : MonoBehaviour{
    public enum e_inputActions {
        move = 0,
        sprint = 1,
        normalAttack = 2,
        specialAttack = 3,
        dodge = 4,
        max,
    };
    private readonly string[] INPUT_ACTIONS_NAMES = new string[] {
        "Move",
        "Sprint",
        "NormalAttack",
        "SpecialAttack",
        "Dodge",
    };

    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private InputAction[] _actions = new InputAction[(int)e_inputActions.max];

    public void SetUp() {
        _actions = new InputAction[(int)e_inputActions.max];
        for (int i = 0; i < (int)e_inputActions.max; i++) {
            _actions[i] = _actionAsset.FindAction(INPUT_ACTIONS_NAMES[i]);
        }
    }

    public Vector2 GetAxis() {
        Vector2 input = _actions[(int)e_inputActions.move].ReadValue<Vector2>();
        return input;
    }
}