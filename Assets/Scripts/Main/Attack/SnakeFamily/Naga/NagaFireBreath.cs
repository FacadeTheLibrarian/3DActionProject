using System.Collections;
using SimpleMan.VisualRaycast;
using UnityEngine;

internal sealed class NagaFireBreath : BaseAttack {

    [SerializeField] private Animator _animator = default;
    [SerializeField] private int _castIteration = 5;
    [SerializeField] private Vector3 _offsetParCast = new Vector3(5.0f, 0.0f, 0.0f);

    private Vector3 _current = default;
    private Vector3 _addend = default;

    //FIXME: リファクタリング必須
    public void FireBreath() {
        _stamina.UseStamina(_baseStaminaConsumption);
        float clipLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        float castInterval = clipLength / _castIteration;
        _current = GetInitialCastPosition();
        int executedCasts = 0;

        Vector3 forward = _direction.GetCachedForward();
        _addend = Quaternion.LookRotation(forward, Vector3.up) * _offsetParCast;

        Transform playerTransform = _playerTransform ;
        Collider[] results = ComponentExtension.BoxOverlap(playerTransform, _current, _boxSize, playerTransform.rotation, _layer, true);
        executedCasts++;
        _current += _addend;

        foreach (Collider collider in results) {
            if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                possibleEnemy.GetHit((int)(_baseDamage * _attackFactor.GetAttackFactor), _direction.GetCachedForward());
            }
        }
        StartCoroutine(OnAttack(executedCasts, castInterval));
    }
    private IEnumerator OnAttack(int alreadyExecutedCasts, float castInterval) {
        int executedCasts = alreadyExecutedCasts;
        float timeAccumeration = 0.0f;
        while (true) {
            if (executedCasts >= _castIteration) {
                yield break;
            }
            timeAccumeration += Time.deltaTime;
            if (timeAccumeration > castInterval) {
                timeAccumeration = 0.0f;

                Transform playerTransform = _playerTransform;
                Collider[] results = ComponentExtension.BoxOverlap(playerTransform, _current, _boxSize, playerTransform.rotation, _layer, true);
                executedCasts++;

                foreach (Collider collider in results) {
                    if (collider.TryGetComponent<IDamagableObjects>(out IDamagableObjects possibleEnemy)) {
                        possibleEnemy.GetHit((int)(_baseDamage * _attackFactor.GetAttackFactor), _direction.GetCachedForward());
                    }
                }
                _current += _addend;
            }
            yield return null;
        }
    }
}