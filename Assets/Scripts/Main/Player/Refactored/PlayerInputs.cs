using System;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerInputs : MonoBehaviour {
    //FIXME: なぜPlayerInputsのe_inputActionsを入力が必要なクラスが知る必要があるのか
    //       オーケストレーションかファサードからCIで渡すべきだと思う
    public enum e_inputActions {
        move = 0,
        sprint = 1,
        normalAttack = 2,
        specialAttack = 3,
        dodge = 4,
        Growth = 5,
        max,
    };
    private readonly string[] INPUT_ACTIONS_NAMES = new string[] {
        "Move",
        "Sprint",
        "NormalAttack",
        "SpecialAttack",
        "Dodge",
        "Growth",
    };

    [SerializeField] private InputActionAsset _actionAsset = default;
    private InputAction[] _actions = new InputAction[(int)e_inputActions.max];

    public InputAction this[e_inputActions index] {
        get { return _actions[(int)index]; }
    }

    public void Initialization() {
        for (int i = 0; i < (int)e_inputActions.max; i++) {
            _actions[i] = _actionAsset.FindAction(INPUT_ACTIONS_NAMES[i]);
        }
    }

    public void StartAcceptInput() {
        foreach (InputAction action in _actions) {
            action.Enable();
        }
    }
}