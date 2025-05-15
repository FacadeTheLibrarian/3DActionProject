using System;
using UniRx;
using UnityEngine;

internal sealed class AnimationStateController: IDisposable{
    private readonly int PARAMS_SPEED = Animator.StringToHash("Speed");
    private readonly int PARAMS_MOVE = Animator.StringToHash("IsOnMove");
    private readonly int PARAMS_SPRINT = Animator.StringToHash("IsOnSprint");
    private readonly int PARAMS_ATTACK = Animator.StringToHash("Attack");

    private readonly IDisposable DISPOSABLE = default;

    private MonsterHandler _monsterHandler = default;
    private MonsterHandler.e_generation _currentGeneration = default;

    public AnimationStateController(in MonsterHandler handler) {
        _monsterHandler = handler;
        DISPOSABLE = handler.CurrentGeneration.Subscribe((MonsterHandler.e_generation nextGeneration) => {
            _currentGeneration = nextGeneration;
        });
    }

    public void Dispose() {
        DISPOSABLE.Dispose();
    }

    public bool IsMoveInvalid {
        get {
            AnimatorStateInfo clipInfo = _monsterHandler[(int)_currentGeneration].GetAnimator.GetCurrentAnimatorStateInfo(0);
            return clipInfo.shortNameHash != PARAMS_MOVE;
        }
    }

    public void SetSpeed(float speed) {
        _monsterHandler[(int)_currentGeneration].GetAnimator.SetFloat(PARAMS_SPEED, speed);
    }

    public float GetSpeed() {
        float speed = _monsterHandler[(int)_currentGeneration].GetAnimator.GetFloat(PARAMS_SPEED);
        return speed;
    }
}