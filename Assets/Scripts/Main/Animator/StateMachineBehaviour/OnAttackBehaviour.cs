using System;
using UnityEngine;

internal class OnAttackBehaviour : StateMachineBehaviour {

    public event Action OnStartAttackPublisher;
    public event Action OnEndAttackPublisher;

    protected IPlayerState _player = default;

    public void Initialization(in IPlayerState player) {
        _player = player;
    }

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnStartAttackPublisher?.Invoke();
    }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnEndAttackPublisher?.Invoke();
    }
}