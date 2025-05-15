using System.Collections.Generic;
using UnityEngine;

internal class BasePlayableMonster : MonoBehaviour {
    public enum e_generation : int {
        first = 0,
        second = 1,
        third = 2,
        max,
    }
    [SerializeField] private LayerMask _attackLayer = default;
    [SerializeField] private Animator _animator = default;
    [SerializeField] private List<BaseAttack> _attacks = new List<BaseAttack>();

#if UNITY_EDITOR
    private void Reset() {
        _animator = this.GetComponent<Animator>();
        _attacks.AddRange(this.gameObject.GetComponents<BaseAttack>());
    }
#endif

    public Animator GetAnimator => _animator;

    public void Initialization(in IPlayer master) {
        foreach (BaseAttack attack in _attacks) {
            attack.Initialization(master, _attackLayer);
        }
    }
}