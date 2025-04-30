using System.Collections.Generic;
using UnityEngine;

internal sealed class NagaBiteAttack : BaseOnAttackAction {

    protected override void OnEnterAttack() {
        Vector3 castPosition = GetCastPosition(3.0f);
        Collider[] results = new Collider[_attackCastVariables.GetBufferSizeMax];
        int numberOfCollider = Physics.OverlapBoxNonAlloc(castPosition, new Vector3(2.0f, 0.5f, 0.5f), results, _player.GetRotation(), _attackCastVariables.GetLayerMask);
        if (numberOfCollider == 0) {
            return;
        }

        for (int i = 0; i < numberOfCollider; i++) {
            if (results[i].TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(10, _player.GetForward());
            }
        }
    }
}

/*
 * 必要なモノ
     private void AttackCast() {
        UseStamina(_staminaConsumptionOnAttack);
        Vector3 forwardedPosition = (_forward * 3.0f);
        Vector3 height = new Vector3(0.0f, this.transform.localScale.y / 2.0f, 0.0f);
        Vector3 castPosition = this.transform.position + height + forwardedPosition;
        Collider[] results = new Collider[CAST_BUFFER_SIZE];
        int numberOfCollider = Physics.OverlapSphereNonAlloc(castPosition, 2.0f, results, _layer);
        //Physics.SphereCastNonAlloc(this.transform.position, 2.0f, _forward, results, 10.0f, _layer);

        if (numberOfCollider == 0) {
            return;
        }

        for (int i = 0; i < numberOfCollider; i++) {
            if (results[i].TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(10, _forward);
            }
        }
    }
 
 */
