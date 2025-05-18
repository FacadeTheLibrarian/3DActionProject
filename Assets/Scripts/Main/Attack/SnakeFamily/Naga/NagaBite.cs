using SimpleMan.VisualRaycast;
using UnityEngine;

internal sealed class NagaBite : BaseAttack {
    public void Bite() {
        _stamina.UseStamina(_baseStaminaConsumption);
        Vector3 castPosition = GetInitialCastPosition();

        Transform playerTransform = _playerTransform;
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, castPosition, _boxSize, playerTransform.rotation, _layer, true);

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit((int)(_baseDamage * _attackFactor.GetAttackFactor), _direction.GetCachedForward());
            }
        }   
    }
}
