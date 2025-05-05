using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeBiteAttack : BaseOnAttackAction {
    protected override void OnEnterAttack(float clipLength) {
        Vector3 castPosition = GetCastPosition(_forwardOffset);
        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, castPosition, _boxSize, playerTransform.rotation, _attackCastVariables.GetLayerMask, true);

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit((int)(_baseDamage * _player.GetAttackFactor()), _player.GetForward());
            }
        }
    }
}