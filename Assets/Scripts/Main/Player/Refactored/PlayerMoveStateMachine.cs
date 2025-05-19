using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerMoveStateMachine : MonoBehaviour {

    //CharacterController.IsGrounded を使う
    [SerializeField] private PlayerHorizontalMove _playerHorizontalMove = default;
    [SerializeField] private PlayerDodge _playerDodge = default;
    [SerializeField] private PlayerVerticalMove _playerVerticalMove = default;
    [SerializeField] private CharacterController _characterController = default;

    private InputAction _dodge = default;
    private InputAction _move = default;
    private AnimationStateController _animationStateController = default;

    private IMoveBehaviour _currentBehaviour = default;

#if UNITY_EDITOR
    private void Reset() {
        _playerDodge = GetComponent<PlayerDodge>();
        _playerHorizontalMove = GetComponent<PlayerHorizontalMove>();
    }
#endif

    public void Initialization(in PlayerInputs inputs, in AnimationStateController animationStateController, in PlayerDirection direction, in PlayerStamina stamina) {
        _playerHorizontalMove.Initialization(animationStateController, direction);
        _playerDodge.Initialization(direction, stamina);
        _playerVerticalMove.Initialization(_characterController);

        _currentBehaviour = _playerHorizontalMove;
        _animationStateController = animationStateController;

        _move = inputs[PlayerInputs.e_inputActions.move];
        _move.started += OnStartMove;
        _move.canceled += OnStopMove;
        _move.Enable();

        _dodge = inputs[PlayerInputs.e_inputActions.dodge];
        _dodge.started += OnStartDodge;
        _dodge.Enable();

        _playerDodge.OnEndDodge += OnEndDodge;
    }

    private void OnDestroy() {
        _move.started -= OnStartMove;
        _move.canceled -= OnStopMove;
        _move.Disable();
        _dodge.started -= OnStartDodge;
        _dodge.Disable();
    }

    public void UpdateBehaviour() {
        Vector2 input = _move.ReadValue<Vector2>();
        Vector3 direction = _currentBehaviour.UpdateMoveBehaviour(input);
        Vector3 verticalVelocity = _playerVerticalMove.GetVerticalSpeed();
        _characterController.Move((direction + verticalVelocity) * Time.deltaTime);
    }

    public void OnStartDodge(InputAction.CallbackContext context) {
        if (_animationStateController.IsOnMovementLock) {
            return;
        }
        _animationStateController.SetSpeed(0.0f);
        _animationStateController.StartDodge();
        _playerDodge.OnStartDodge(_move.ReadValue<Vector2>());
        _currentBehaviour = _playerDodge;
    }
    public void OnEndDodge() {
        _currentBehaviour = _playerHorizontalMove;
        _animationStateController.EndDodge();
    }
    public void OnStartMove(InputAction.CallbackContext context) {
        _playerHorizontalMove.OnStartMove();
    }
    public void OnStopMove(InputAction.CallbackContext context) {
        _playerHorizontalMove.OnStopMove();
    }
}