using System;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class TitleInput : IDisposable {
    readonly private InputActionAsset _inputAssets = default;
    readonly private InputAction _move = default;
    readonly private InputAction _jump = default;

    public Vector2 MoveDirection {
        get;
        private set;
    }

    public bool IsJumpDoublePressed {
        get;
        private set;
    }

    public bool IsJumpSinglePressed {
        get;
        private set;
    }

    public TitleInput(in InputActionAsset assets) {
        _inputAssets = assets;

        _move = _inputAssets.FindAction("Move");
        _move.performed += OnMove;

        _jump = _inputAssets.FindAction("Jump");
        _jump.started += OnJumpSinglePressed;
        _jump.performed += OnJumpDoublePressed;

        _inputAssets.Enable();
    }

    private void OnMove(InputAction.CallbackContext context) {
        MoveDirection = context.ReadValue<Vector2>();
    }

    private void OnJumpSinglePressed(InputAction.CallbackContext context) {
        Debug.Log("Single");
    }

    private void OnJumpDoublePressed(InputAction.CallbackContext context) {
        Debug.Log("Double");
    }

    public void Dispose() {
        _move.performed -= OnMove;
        _jump.started -= OnJumpSinglePressed;
        _jump.performed -= OnJumpDoublePressed;
        _inputAssets.Disable();
    }
}