using System.Collections;
using UnityEngine;

internal class BasePlayerHomingProjectile : MonoBehaviour, IPoolableObjects {
    private bool _isOccupied = false;
    public bool GetIsOccupied() => _isOccupied;

    [SerializeField] private ParticleSystem _baseProjectileParticleHandler = default;

    [SerializeField] private RaycastHit _target = default;
    [SerializeField] private Vector3 _targetPosition = default;
    [SerializeField] private Vector3 _forward = default;
    [SerializeField] private Vector3 _right = default;

    [SerializeField] private Vector3 _velocity = default;

    [SerializeField] private int _baseProjectileDamageAmount = 0;
    [SerializeField] protected int _subProjectileDamageAmount = 0;

    [SerializeField] private bool _isTargetSet = false;

    [SerializeField] private int _seekCounter = 0;
    [SerializeField] private float _maximumVelocity = 50.0f;
    [SerializeField] private float _lifetime = 0.0f;
    [SerializeField] private float _timeLeftToHit = 0.0f;

    [SerializeField] private LayerMask _layer = default;

    public void Fire(in Vector3 currentPosition, in Vector3 forward, float initialVelocity, int mainDamage, int subDamage, in LayerMask layer) {
        _isTargetSet = false;

        this.transform.position = currentPosition;

        _forward = forward;
        _velocity = forward * initialVelocity;

        _baseProjectileDamageAmount = mainDamage;
        _subProjectileDamageAmount = subDamage;

        _lifetime = _baseProjectileParticleHandler.main.duration;
        _timeLeftToHit = _lifetime;

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
        if (difference.magnitude < 0.5f) {
            End();
        }
        if (_lifetime - _timeLeftToHit >= _lifetime) {
            End();
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
    }

    private IEnumerator OnShot() {
        yield return null;
        while (true) {
            Homing();
            _timeLeftToHit -= Time.deltaTime;
            if (_lifetime - _timeLeftToHit >= _lifetime) {
                _baseProjectileParticleHandler.Stop();
                _isOccupied = false;
                yield break;
            }
            yield return null;
        }
    }
}