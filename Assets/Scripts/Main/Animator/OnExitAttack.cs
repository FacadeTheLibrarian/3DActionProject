using System.Collections.Generic;
using UnityEngine;

public class OnExitAttack : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("Attack");
    }
}