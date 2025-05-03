using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeletBiteAttack : BaseOnAttackAction {
	    protected override void OnEnterAttack(float clipLength) {
        Vector3 castPosition = GetCastPosition(3.0f);

        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, castPosition, new Vector3(2.5f, 1.0f, 2.0f), playerTransform.rotation, _attackCastVariables.GetLayerMask, true);

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(10, _player.GetForward());
            }
        }
    }
}