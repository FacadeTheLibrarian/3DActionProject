using System;
using UnityEngine;

internal class BaseOnAttackAction : StateMachineBehaviour {

    public event Action OnStartAttackPublisher;
    public event Action OnEndAttackPublisher;

    [SerializeField] protected AttackCastVariables _attackCastVariables;
    protected IPlayer _player = default;

    public void Initialization(in IPlayer player) {
        _player = player;
    }

    protected Vector3 GetCastPosition() {
        Vector3 twoDimentionalCastPosition = _player.GetForward() * _attackCastVariables.GetDistanceFactor;
        Vector3 castPosition = _player.GetPosition() + new Vector3(twoDimentionalCastPosition.x, _player.GetHalfScaleY(), twoDimentionalCastPosition.z);
        return castPosition;
    }
    protected Vector3 GetCastPosition(float factor) {
        Vector3 twoDimentionalCastPosition = _player.GetForward() * factor;
        Vector3 castPosition = _player.GetPosition() + new Vector3(twoDimentionalCastPosition.x, _player.GetHalfScaleY(), twoDimentionalCastPosition.z);
        return castPosition;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnStartAttackPublisher?.Invoke();
        OnEnterAttack();
    }

    protected virtual void OnEnterAttack() {
        //nop
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnAttackUpdate();
    }

    protected virtual void OnAttackUpdate() {
        //nop
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnEndAttackPublisher?.Invoke();
        OnEndAttack();
    }

    protected virtual void OnEndAttack() {
        //nop
    }
}