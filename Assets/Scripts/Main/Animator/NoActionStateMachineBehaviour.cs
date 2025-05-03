using System;
using UnityEngine;

internal class NoActionStateMachineBehaviour : StateMachineBehaviour {

    public event Action OnStartAttackPublisher;
    public event Action OnEndAttackPublisher;

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnStartAttackPublisher?.Invoke();
    }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnEndAttackPublisher?.Invoke();
    }
}