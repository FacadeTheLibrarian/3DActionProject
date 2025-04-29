using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerAnimatorHandler {
    private const string PARAMETER_SPEED = "Speed";

    private Animator _animator;

    public PlayerAnimatorHandler(in Animator handler) {
        _animator = handler;
    }

    public void SetSpeed(float speed) {
        _animator.SetFloat(PARAMETER_SPEED, speed);
    }
}