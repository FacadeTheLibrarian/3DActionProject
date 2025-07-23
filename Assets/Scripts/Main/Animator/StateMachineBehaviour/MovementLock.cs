using System.Collections.Generic;
using UnityEngine;

internal sealed class MovementLock : StateMachineBehaviour {
    private readonly int MOVEMENT_LOCK = Animator.StringToHash("MovementLock");
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool(MOVEMENT_LOCK, true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool(MOVEMENT_LOCK, false);
    }
}