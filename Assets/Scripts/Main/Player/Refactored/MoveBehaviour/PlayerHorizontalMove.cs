using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerHorizontalMove : MonoBehaviour, IMoveBehaviour {
    private const float MOVE_SPEED = 10.0f;
    private const float ANIM_STOP = 0.0078125f;
    private const float STOP_THRESHOLD = 0.125f;
    private const float STOP_DAMP = 0.25f;
    private const float MOVE_DAMP = 0.25f;

    private Func<Vector2, Vector3> _currentBehaviour = default;

    private AnimationStateController _animationStateController = default;
    private PlayerDirection _playerDirection = default;

    private float _currentMaxSpeed = MOVE_SPEED;

    public void Initialization(in AnimationStateController animationStateController, in PlayerDirection direction) {
        _animationStateController = animationStateController;
        _playerDirection = direction;

        _currentBehaviour = Idle;
    }
    public Vector3 UpdateMoveBehaviour(Vector2 inputDirection) {
        if (_animationStateController.IsMoveInvalid) {
            return Vector3.zero;
        }
        Vector3 direction = _currentBehaviour.Invoke(inputDirection);
        return direction;
    }
    public void OnStartMove() {
        _currentBehaviour = OnInput;
    }
    public void OnStopMove() {
        _currentBehaviour = OnNoInput;
    }
    private Vector3 OnInput(Vector2 inputDirection) {
        float magnitude = Mathf.Clamp01(inputDirection.magnitude);
        Vector3 forward = _playerDirection.GetUpdatedForward(inputDirection);

        float speed = magnitude * _currentMaxSpeed;

        Vector3 force = new Vector3(forward.x * speed, 0.0f, forward.z * speed);
        _animationStateController.SetSpeed(magnitude, MOVE_DAMP);
        return force;
    }
    private Vector3 OnNoInput(Vector2 inputDirection) {
        _animationStateController.SetSpeed(0.0f, STOP_DAMP);
        Vector3 forward = _playerDirection.GetCachedForward();
        float nextSpeed = _animationStateController.GetSpeed();
        if (nextSpeed <= STOP_THRESHOLD) {
            _currentBehaviour = OnAnimationLerp;
            return Vector3.zero;
        }
        Vector3 force = new Vector3(forward.x * nextSpeed, 0.0f, forward.z * nextSpeed);
        return force;
    }
    private Vector3 OnAnimationLerp(Vector2 inputDirection) {
        _animationStateController.SetSpeed(0.0f, STOP_DAMP);
        float nextSpeed = _animationStateController.GetSpeed();
        if (nextSpeed <= ANIM_STOP) {
            _currentBehaviour = Idle;
        }
        return Vector3.zero;
    }
    private Vector3 Idle(Vector2 inputDirection) {
        return Vector3.zero;
    }
}