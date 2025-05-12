using UnityEngine;

internal sealed class OnExitAttack : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("Attack");
    }
}