using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class NagaRightSlashAttack : BaseOnAttackAction {
    protected override void OnEnterAttack(float clipLength) {
        Vector3 forward = _player.GetForward();
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        Vector3 castPosition = GetCastPosition() + right * _attackCastVariables.GetRightAdjustment;
        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, castPosition, new Vector3(3.0f, 1.0f, 3.0f), playerTransform.rotation, _attackCastVariables.GetLayerMask, true);

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(5, _player.GetForward());
            }
        }
    }
}
