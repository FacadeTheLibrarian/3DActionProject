using System.Collections.Generic;
using UnityEngine;

internal sealed class NagaRightSlashAttack : BaseOnAttackAction {
	    protected override void OnEnterAttack() {
        Vector3 castPosition = GetCastPosition() + _attackCastVariables.GetRightAdjustment;
        Collider[] results = new Collider[_attackCastVariables.GetBufferSizeMax];
        int numberOfCollider = Physics.OverlapBoxNonAlloc(castPosition, new Vector3(0.25f, 0.25f, 0.25f), results, _player.GetRotation(), _attackCastVariables.GetLayerMask);
        if (numberOfCollider == 0) {
            return;
        }

        for (int i = 0; i < numberOfCollider; i++) {
            if (results[i].TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(5, _player.GetForward());
            }
        }
    }
}