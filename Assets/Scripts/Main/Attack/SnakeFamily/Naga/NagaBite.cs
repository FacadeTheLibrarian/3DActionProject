using System.Collections.Generic;
using UnityEngine;
using SimpleMan.VisualRaycast;

internal sealed class NagaBite : BaseAttack {

    public void Bite() {
        Vector3 castPosition = GetInitialCastPosition();

        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, castPosition, _boxSize, playerTransform.rotation, _layer, true);

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit((int)(_baseDamage * _player.GetAttackFactor()), _player.GetForward());
            }
        }
    }
}
