using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeletProjectile : BasePlayerCruisingProjectile {
    [SerializeField] private ParticleSystem _explosion = default;
    [SerializeField] private float _explosionRadius = 1.5f;

    protected override void OnHitProjectile() {
        Collider[] results = ComponentExtension.SphereOverlap(this.transform, this.transform.position, _explosionRadius, _layer, true);
        foreach (Collider collider in results) {
            if (collider.TryGetComponent(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(_subProjectileDamageAmount, _forward);
            }
        }

        _explosion.gameObject.SetActive(true);
        _explosion.Play();
    }
}