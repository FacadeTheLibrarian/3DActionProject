using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerMove : MonoBehaviour {
    private const float MOVE_SPEED = 10.0f;
    private const float ANIM_STOP = 0.0078125f;
    private const float STOP_THRESHOLD = 0.125f;
    private const float STOP_DAMP = 0.25f;
    private const float MOVE_DAMP = 0.25f;
    private const float GROUND_OFFSET = -0.5f;
    private const float GROUND_APPROXIMATELY = 0.5f;
    private const float FALL_SPEED_ADDEND = 9.81f;
    private const float VERTICAL_TERMINAL_VELOCITY = 10.0f;

    private Action _currentBehaviour = default;

    [SerializeField] private CharacterController _characterController = default;

    private InputAction _move = default;
    private AnimationStateController _animationStateController = default;
    private PlayerDirection _playerDirection = default;
    private GroundCheck _groundCheck = default;

    private LayerMask _terrainLayer = (int)LayerNumbers.e_layers.staticTerrain;

    private float _currentMaxSpeed = MOVE_SPEED;
    private float _currentVerticalVelocity = 0.0f;

#if UNITY_EDITOR
    private void Reset() {
        _characterController = GetComponent<CharacterController>();
    }
#endif
    public void Initialization(in PlayerInputs inputs, in AnimationStateController animationStateController, in PlayerDirection direction) {
        _move = inputs[PlayerInputs.e_inputActions.move];

        _move.started += OnStartMove;
        _move.canceled += OnStopMove;
        _move.Enable();

        _animationStateController = animationStateController;
        _playerDirection = direction;
        _groundCheck = new GroundCheck();

        _currentBehaviour = Idle;
    }

    private void OnDestroy() {
        _move.started -= OnStartMove;
        _move.canceled -= OnStopMove;
        _move.Disable();
    }

    public void UpdateMove() {
        if (_animationStateController.IsMoveInvalid) {
            return;
        }
        _currentBehaviour?.Invoke();
    }

    public float GetVerticalSpeed(float currentVelocity) {
        bool isOnGround = _groundCheck.GroundedCheck(this.transform.position, GROUND_OFFSET, GROUND_APPROXIMATELY, _terrainLayer);
        if (isOnGround) {
            return 0.0f;
        }
        if (currentVelocity < VERTICAL_TERMINAL_VELOCITY) {
            float nextVelocity = currentVelocity + FALL_SPEED_ADDEND * Time.deltaTime;
            return nextVelocity;
        }
        return currentVelocity;
    }

    public void OnStartMove(InputAction.CallbackContext context) {
        _currentBehaviour = OnInput;
    }
    public void OnStopMove(InputAction.CallbackContext context) {
        _currentBehaviour = OnNoInput;
    }

    private void OnInput() {
        Vector2 input = _move.ReadValue<Vector2>();
        float magnitude = Mathf.Clamp01(input.magnitude);
        Vector3 forward = _playerDirection.GetUpdatedForward(input);

        float speed = magnitude * _currentMaxSpeed;
        _currentVerticalVelocity = GetVerticalSpeed(_currentVerticalVelocity);

        Vector3 force = new Vector3(forward.x * speed, -_currentVerticalVelocity, forward.z * speed);
        _animationStateController.SetSpeed(magnitude, MOVE_DAMP);
        _characterController.Move(force * Time.deltaTime);
    }
    private void OnNoInput() {
        _animationStateController.SetSpeed(0.0f, STOP_DAMP);
        Vector3 forward = _playerDirection.GetCachedForward();
        float nextSpeed = _animationStateController.GetSpeed();
        if (nextSpeed <= STOP_THRESHOLD) {
            _currentBehaviour = OnAnimationLerp;
            return;
        }
        Vector3 force = new Vector3(forward.x * nextSpeed, -_currentVerticalVelocity, forward.z * nextSpeed);
        _characterController.Move(force * Time.deltaTime);
    }

    private void OnAnimationLerp() {
        _animationStateController.SetSpeed(0.0f, STOP_DAMP);
        float nextSpeed = _animationStateController.GetSpeed();
        if (nextSpeed <= ANIM_STOP) {
            _currentBehaviour = Idle;
            return;
        }
    }

    private void Idle() {
        return;
    }
}