using System.Collections;
using UnityEngine;

internal class PlayerTimeBombProjectile : BaseProjectile {

    [SerializeField] private ParticleSystem _baseProjectileParticleHandler = default;

    [SerializeField] private Vector3 _targetPosition = default;
    [SerializeField] protected Vector3 _forward = default;

    [SerializeField] private Vector3 _velocity = default;

    [SerializeField] private int _baseProjectileDamageAmount = 0;
    [SerializeField] protected int _subProjectileDamageAmount = 0;

    [SerializeField] private float _maximumVelocity = 50.0f;
    [SerializeField] private float _timer = 0.0f;
    [SerializeField] private float _timeLeftToHit = 0.0f;
    [SerializeField] private bool _isLifeTimeOver = false;

    [SerializeField] protected LayerMask _layer = default;

    public void Fire(in Vector3 currentPosition, in Vector3 forward, in Vector3 staticTargetPosition, in Vector3 initialVelocity, int mainDamage, int subDamage, in LayerMask layer, float timer) {
        _isLifeTimeOver = false;
        this.transform.position = currentPosition;

        _forward = forward;
        _targetPosition = staticTargetPosition;
        _velocity = initialVelocity;

        _baseProjectileDamageAmount = mainDamage;
        _subProjectileDamageAmount = subDamage;

        _timer = timer;
        _timeLeftToHit = _timer;

        _layer = layer;

        _isOccupied = true;
        _baseProjectileParticleHandler.Play();
        StartCoroutine(OnShot());
    }


    protected virtual void OnHitProjectile() {
        //nop
    }

    private void Homing() {
        Vector3 currentPosition = this.transform.position;

        Vector3 acceleration = Vector3.zero;
        Vector3 difference = _targetPosition - currentPosition;
        acceleration += (difference - (_velocity * _timeLeftToHit)) * 2.0f / (_timeLeftToHit * _timeLeftToHit);

        _timeLeftToHit -= Time.deltaTime;
        if (_timer - _timeLeftToHit >= _timer) {
            End();
            return;
        }
        if (_velocity.magnitude < _maximumVelocity) {
            _velocity += acceleration * Time.deltaTime;
        }
        currentPosition += _velocity * Time.deltaTime;
        this.transform.position = currentPosition;
    }

    private void End() {
        _baseProjectileParticleHandler.Stop();
        _isOccupied = false;
        _isLifeTimeOver = true;
    }

    private IEnumerator OnShot() {
        yield return null;
        while (true) {
            Homing();
            if (_isLifeTimeOver) {
                OnHitProjectile();
                yield break;
            }
            yield return null;
        }
    }
}