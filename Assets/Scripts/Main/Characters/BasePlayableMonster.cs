using System.Collections.Generic;
using UnityEngine;

internal class BasePlayableMonster : MonoBehaviour {
    [SerializeField] private Animator _animator = default;
    [SerializeField] private List<BaseAttack> _attacks = new List<BaseAttack>();

    public Animator GetAnimator => _animator;

#if UNITY_EDITOR
    private void Reset() {
        _animator = this.GetComponent<Animator>();
        _attacks.AddRange(this.gameObject.GetComponents<BaseAttack>());
    }
#endif

    public void SetAttack(in Transform playerPosition, in PlayerDirection direction, in PlayerAttackFactor attackFactor, in PlayerStamina stamina) {
        foreach (BaseAttack attack in _attacks) {
            attack.Initialization(playerPosition, direction, attackFactor, stamina);
        }
    }
}