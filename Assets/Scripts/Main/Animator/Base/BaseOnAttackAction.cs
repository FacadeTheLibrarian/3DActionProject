using System;
using UnityEngine;

internal class BaseOnAttackAction : StateMachineBehaviour {

    public event Action OnStartAttackPublisher;
    public event Action OnEndAttackPublisher;

    [SerializeField] protected Vector3 _boxSize = default;
    [SerializeField, Range(1.0f, 5.0f)] protected float _forwardOffset = default;
    [SerializeField] protected AttackCastVariables _attackCastVariables = default;
    [SerializeField, Range(0, 100)] protected int _baseDamage = default;
    protected IPlayer _player = default;

    public void Initialization(in IPlayer player) {
        _player = player;
        InnerInitialization();
    }

    protected virtual void InnerInitialization() {
        //nop
    }
    protected Vector3 GetCastPosition(float offsetFactor) {
        Vector3 twoDimentionalCastPosition = _player.GetForward() * offsetFactor;
        Vector3 castPosition = _player.GetTransform().position + new Vector3(twoDimentionalCastPosition.x, _player.GetScaleY(), twoDimentionalCastPosition.z);
        return castPosition;
    }

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnStartAttackPublisher?.Invoke();
        OnEnterAttack(stateInfo.length);
    }

    protected virtual void OnEnterAttack(float clipLength) {
        //nop
    }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnAttackUpdate();
    }

    protected virtual void OnAttackUpdate() {
        //nop
    }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnEndAttackPublisher?.Invoke();
        OnEndAttack();
    }

    protected virtual void OnEndAttack() {
        //nop
    }
}