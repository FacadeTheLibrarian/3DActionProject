using System;
using UnityEngine;

internal class StateMachineActionRoot : StateMachineBehaviour {

    public event Action OnStartAttack;
    public event Action OnEndAttack;

    private IPlayer _player = default;

    public void Initialization(in IPlayer player) {
        _player = player;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnStartAttack?.Invoke();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OnEndAttack?.Invoke();
    }
}