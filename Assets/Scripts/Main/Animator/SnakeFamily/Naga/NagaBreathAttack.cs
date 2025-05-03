using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal sealed class NagaBreathAttack : BaseOnAttackAction {

    [SerializeField] private Vector3 _current = default;
    private Vector3 _addend = default;
    private int _numberOfCasts = 5;
    private int _executedCasts = 0;
    private float _timeAccumeration = 0.0f;
    private float _castParSeconds = 0.0f;
    

    //FIXME: リファクタリング必須
    protected override void OnEnterAttack(float clipLength) {
        _castParSeconds = clipLength / _numberOfCasts;

        Vector3 forward = _player.GetForward();
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x) * 5.0f;
        Vector3 left = new Vector3(-forward.z, 0.0f, forward.x) * 5.0f;
        Vector3 difference = (left) - (right);
        _addend = difference / ((float)_numberOfCasts - 1.0f);
        _current = forward * 4.0f + right;
        _executedCasts = 0;

        Transform playerTransform = _player.GetTransform();
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, playerTransform.position + _current, new Vector3(4.0f, 1.0f, 6.0f), playerTransform.rotation, _attackCastVariables.GetLayerMask, true);
        _executedCasts++;
        _current += _addend;

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit(10, _player.GetForward());
            }
        }

    }
    protected override void OnAttackUpdate() {
        if (_executedCasts >= _numberOfCasts) {
            return;
        }
        _timeAccumeration += Time.deltaTime;
        if (_timeAccumeration > _castParSeconds) {
            _timeAccumeration = 0.0f;

            Transform playerTransform = _player.GetTransform();
            Collider[] results = ComponentExtension.BoxOverlap(playerTransform, playerTransform.position + _current, new Vector3(4.0f, 1.0f, 6.0f), playerTransform.rotation, _attackCastVariables.GetLayerMask, true);
            _executedCasts++;

            foreach (Collider collider in results) {
                if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                    possibleEnemy.GetHit(10, _player.GetForward());
                }
            }
            _current += _addend;
        }
    }
}