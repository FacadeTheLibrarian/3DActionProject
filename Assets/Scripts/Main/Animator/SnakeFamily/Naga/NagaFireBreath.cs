using SimpleMan.VisualRaycast;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal sealed class NagaFireBreath : BaseAttack {

    [SerializeField] private Animator _animator = default;
    [SerializeField] private int _castIteration = 5;
    [SerializeField] private Vector3 _offsetParCast = new Vector3(5.0f, 0.0f, 0.0f);

    private Vector3 _current = default;
    private Vector3 _addend = default;
    private int _executedCasts = 0;
    private float _timeAccumeration = 0.0f;
    private float _castInterval = 0.0f;
    

    //FIXME: リファクタリング必須
    public void FireBreath() {
        float clipLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        _castInterval = clipLength / _castIteration;
        _current = GetInitialCastPosition();
        _executedCasts = 0;

        Vector3 forward = _player.GetForward();
        _addend = Quaternion.LookRotation(forward, Vector3.up) * _offsetParCast;

        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, _current, _boxSize, playerTransform.rotation, _layer, true);
        _executedCasts++;
        _current += _addend;

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit((int)(_baseDamage * _player.GetAttackFactor()), _player.GetForward());
            }
        }
        StartCoroutine(OnAttack());
    }
    private IEnumerator OnAttack() {
        while (true) {
            if (_executedCasts >= _castIteration) {
                yield break;
            }
            _timeAccumeration += Time.deltaTime;
            if (_timeAccumeration > _castInterval) {
                _timeAccumeration = 0.0f;

                Transform playerTransform = _player.GetTransform();
                Collider[] results = ComponentExtension.BoxOverlap(playerTransform, _current, _boxSize, playerTransform.rotation, _layer, true);
                _executedCasts++;

                foreach (Collider collider in results) {
                    if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                        possibleEnemy.GetHit((int)(_baseDamage * _player.GetAttackFactor()), _player.GetForward());
                    }
                }
                _current += _addend;
            }
            yield return null;
        }
    }
}