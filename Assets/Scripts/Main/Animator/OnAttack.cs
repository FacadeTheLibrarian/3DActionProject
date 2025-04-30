using System;
using UnityEngine;

public class OnAttack : StateMachineBehaviour {

    public event Action OnStartAttack;
    public event Action OnEndAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnStartAttack?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnEndAttack?.Invoke();
    }
}