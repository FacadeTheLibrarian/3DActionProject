using System.Collections.Generic;
using UnityEngine;
using SimpleMan.VisualRaycast;

internal sealed class NagaBiteAttack : BaseOnAttackAction {

    protected override void OnEnterAttack(float clipLength) {
        Vector3 castPosition = GetCastPosition(3.0f);


        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, castPosition, new Vector3(5.0f, 1.0f, 3.0f), playerTransform.rotation, _attackCastVariables.GetLayerMask, true);

        //CastResult result = ComponentExtension.Boxcast(playerTransform, true, playerTransform.position, _player.GetForward(), new Vector3(5.0f, 1.0f, 3.0f), 5.0f, _attackCastVariables.GetLayerMask, true);

        //if (!result) {
        //    Debug.Log("BoxCastヒットなし");
        //    return;
        //}

        //foreach (RaycastHit hit in result.Hits) {
        //    if (hit.collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
        //        possibleEnemy.GetHit(10, _player.GetForward());
        //    }
        //}

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
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
