using SimpleMan.VisualRaycast;
using UnityEngine;
internal sealed class NagaRightSlash : BaseAttack {
    public void RightSlash() {
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
