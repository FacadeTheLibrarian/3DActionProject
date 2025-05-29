using System;
using UniRx;
using UnityEngine;

internal sealed class AnimationStateController: MonoBehaviour {
    private readonly int NAME_LOCOMOTION = Animator.StringToHash("Locomotion");
    private readonly int NAME_GROWTH = Animator.StringToHash("Growth");

    private readonly int PARAMS_SPEED = Animator.StringToHash("Speed");
    private readonly int PARAMS_ATTACK = Animator.StringToHash("Attack");
    private readonly int PARAMS_ATTACK_TYPE = Animator.StringToHash("AttackType");
    private readonly int PARAMS_START_DODGE = Animator.StringToHash("Dodge");
    private readonly int PARAMS_END_DODGE = Animator.StringToHash("EndDodge");
    private readonly int PARAMS_MOVEMENT_LOCK = Animator.StringToHash("MovementLock");

    public event Action OnEventSceneStart = default;

    private MonsterHandler _monsterHandler = default;
    private PlayerGeneration _playerGeneration = default;
    public void Initialization(in MonsterHandler handler, in PlayerGeneration playerGeneration) {
        _monsterHandler = handler;
        _playerGeneration = playerGeneration;
    }

    public bool IsMoveInvalid {
        get {
            AnimatorStateInfo clipInfo = _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator.GetCurrentAnimatorStateInfo(0);
            return clipInfo.shortNameHash != NAME_LOCOMOTION;
        }
    }

    public bool IsOnMovementLock {
        get {
            return _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator.GetBool(PARAMS_MOVEMENT_LOCK);
        }
    }

    public void SetSpeedImmidiately(float speed) {
        _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator.SetFloat(PARAMS_SPEED, speed);
    }

    public void SetSpeed(float speed, float dampTime) {
        _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator.SetFloat(PARAMS_SPEED, speed, dampTime, Time.deltaTime);
    }
    public float GetSpeed() {
        return _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator.GetFloat(PARAMS_SPEED);
    }
    public void SetAttack(int attackType) {
        Animator animator = _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator;
        animator.SetTrigger(PARAMS_ATTACK);
        animator.SetInteger(PARAMS_ATTACK_TYPE, attackType);
    }

    public void StartDodge() {
        Animator animator = _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator;
        animator.SetTrigger(PARAMS_START_DODGE);
    }
    public void EndDodge() {
        Animator animator = _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator;
        animator.SetTrigger(PARAMS_END_DODGE);
    }
    public void StartGrowthEvent() {
        Animator animator = _monsterHandler[(int)_playerGeneration.GetCurrentGeneration].GetAnimator;
        animator.Play(NAME_GROWTH);
    }
}