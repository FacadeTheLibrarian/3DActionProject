using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal sealed class PlayerMove : IPlayerAction, IDisposable {
    private const e_inputActions MYSELF = e_inputActions.move;

    private readonly float BASE_MOVE_SPEED = default;
    private readonly float BASE_SPRINT_SPEED = default;
    private readonly float BASE_SPEED_CHANGE_RATE = default;
    private readonly float BASE_STAMINA_CONSUMPTION_ON_SPLINT = default;
    private readonly float STOP_THRESHOLD = default;

    private readonly InputAction _moveAction = default;
    private readonly PlayerAnimatorHandler _animator = default;

    private float _currentHorizontalVelocity = 0.0f;
    private float _staminaConsumptionOnSplint = 0.0f;

    private bool _hasInput = false;
    private bool _onStop = false;

    public PlayerMove(in PlayerMoveVariables variables, in InputAction moveAction, in PlayerAnimatorHandler handler) {
        BASE_MOVE_SPEED = variables.GetBaseMoveSpeed;
        BASE_SPRINT_SPEED = variables.GetBaseSprintSpeed;
        BASE_SPEED_CHANGE_RATE = variables.GetBaseSpeedChangeRate;
        BASE_STAMINA_CONSUMPTION_ON_SPLINT = variables.GetBaseStaminaConsumptionOnSplint;
        STOP_THRESHOLD = variables.GetStopThreshold;

        _moveAction = moveAction;
        _animator = handler;

        _moveAction.started += StartMove;
        _moveAction.canceled += EndMove;
    }

    public void Dispose() {

    }

    public e_inputActions Act(ref GameObject handler, ref PlayerRoot parent) {
        if (_onStop) {
            return MYSELF;
        }
        if (!_hasInput) {
            Deceleration();
            return MYSELF;
        }
        return MYSELF;
    }

    private void Deceleration() {

        if (MathUtility.IsInsideExclusive(_currentHorizontalVelocity, -STOP_THRESHOLD, STOP_THRESHOLD)) {
            _animator.SetSpeed(0.0f);
            _currentHorizontalVelocity = 0.0f;
            _onStop = true;
        }
    }

    private void StartMove(InputAction.CallbackContext context) {
        _onStop = false;
        _hasInput = true;
    }
    private void EndMove(InputAction.CallbackContext context) {
        _hasInput = false;
    }
}