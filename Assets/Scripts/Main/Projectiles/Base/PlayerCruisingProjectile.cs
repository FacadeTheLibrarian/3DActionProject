using SimpleMan.VisualRaycast;
using System;
using System.Collections;
using UnityEngine;

internal class PlayerCruisingProjectile : BaseProjectile {

    [SerializeField] private ParticleSystem _baseProjectileParticleHandler = default;

    [SerializeField] protected Vector3 _forward = default;
    [SerializeField] private Vector3 _velocity = default;

    [SerializeField] private int _baseProjectileDamageAmount = 0;
    [SerializeField] protected int _subProjectileDamageAmount = 0;

    [SerializeField] private int _seekCounter = 0;
    [SerializeField] private float _lifetime = 0.0f;
    [SerializeField] private bool _isLifeTimeOver = false;
    [SerializeField] private float _seekRadius = 1.0f;
    [SerializeField] private int _seekInterval = 3;

    [SerializeField] protected LayerMask _layer = default;

    public void Fire(in Vector3 currentPosition, in Vector3 forward, float velocity, int mainDamage, int subDamage, in LayerMask layer) {
        _isLifeTimeOver = false;
        this.transform.position = currentPosition;

        _forward = forward;
        _velocity = forward * velocity;

        _baseProjectileDamageAmount = mainDamage;
        _subProjectileDamageAmount = subDamage;

        _lifetime = _baseProjectileParticleHandler.main.duration;

        _layer = layer;

        _isOccupied = true;
        _baseProjectileParticleHandler.Play();
        StartCoroutine(OnShot());
    }

    protected virtual void OnHitProjectile() {
        //nop
    }

    private void Cruising() {
        _lifetime -= Time.deltaTime;
        this.transform.position += _velocity * Time.deltaTime;
        if (_lifetime <= 0.0f) {
            End();
        }
    }

    private void Seek() {
        _seekCounter = (_seekCounter + 1) & _seekInterval;
        if (_seekCounter != _seekInterval) {
            return;
        }

        CastResult result = ComponentExtension.SphereCast(this.transform, false, this.transform.position, _forward, _seekRadius, 0.0f, _layer, true);
        if (result) {
            if (result.FirstHit.collider.TryGetComponent(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(_baseProjectileDamageAmount, _forward);
                End();
                return;
            }
        }
    }

    private void End() {
        _baseProjectileParticleHandler.Stop();
        _isOccupied = false;
        _isLifeTimeOver = true;
    }

    private IEnumerator OnShot() {
        while (true) {
            Cruising();
            Seek();
            if (_isLifeTimeOver) {
                OnHitProjectile();
                yield break;
            }
            yield return null;
        }
    }
}