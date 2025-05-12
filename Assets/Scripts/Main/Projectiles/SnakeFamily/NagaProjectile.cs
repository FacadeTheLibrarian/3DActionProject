using SimpleMan.VisualRaycast;
using UnityEngine;

internal sealed class NagaProjectile : PlayerTimeBombProjectile {
    [SerializeField] private ParticleSystem _explosion = default;
    [SerializeField] private float _explosionRadius = 4.0f;

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