using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerHorizontalMove : MonoBehaviour {
    private const float MOVE_SPEED = 10.0f;
    private const float STOP_THRESHOLD = 0.125f;
    private const float SPEED_CHANGE_RATE = 0.0625f;
    private const float GROUND_OFFSET = -0.5f;
    private const float GROUND_APPROXIMATELY = 0.5f;
    private const float FALL_SPEED_ADDEND = 9.81f;
    private const float VERTICAL_TERMINAL_VELOCITY = 10.0f;

    [SerializeField] private InputAction _move = default;
    [SerializeField] private CharacterController _controller = default;
    [SerializeField] private AnimationStateController _animationStateController = default;

    [SerializeField] private PlayerDirection _playerDirection = default;
    private GroundCheck _groundCheck = default;

    [SerializeField] private LayerMask _terrainLayer = 128;//LayerMask.NameToLayer("StaticTerrain");

    [SerializeField] private float _currentMaxSpeed = MOVE_SPEED;
    [SerializeField] private float _currentVerticalVelocity = 0.0f;

    public void Initialization(in PlayerInputs inputs, in AnimationStateController controller, PlayerDirection direction) {
        _move = inputs[PlayerInputs.e_inputActions.move];

        _move.performed += Move;
        _move.canceled += EndMove;

        _animationStateController = controller;
        _playerDirection = direction;
        _groundCheck = new GroundCheck();
    }

    private void OnDestroy() {
        _move.performed -= Move;
        _move.canceled -= EndMove;
        _move.Disable();
    }

    public void Move(InputAction.CallbackContext context) {
        if (_animationStateController.IsMoveInvalid) {
            return;
        }
        Vector2 input = _move.ReadValue<Vector2>();
        float magnitude = input.magnitude;
        Vector3 forward = _playerDirection.GetForward(input);

        float speed = magnitude * _currentMaxSpeed;
        _currentVerticalVelocity = GetVerticalSpeed(_currentVerticalVelocity);

        Vector3 force = new Vector3(forward.x * speed, -_currentVerticalVelocity, forward.z * speed);
        _animationStateController.SetSpeed(magnitude);
        _controller.Move(force * Time.deltaTime);
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

    private void EndMove(InputAction.CallbackContext context) {
        _animationStateController.SetSpeed(0.0f);
    }
}