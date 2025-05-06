using System.Collections.Generic;
using UnityEngine;

internal sealed class NagaTripletFireBall : BaseAttack {
    private const int QUEUE_MAX = 12;

    private Pool<BasePlayerBomberProjectile> _pool = default;
    [SerializeField] private BasePlayerBomberProjectile _prefab = default;

    [SerializeField, Range(0, 100)] private int _explosiveDamage = 5;
    [SerializeField] private float _initialVelocity = 30.0f;


    [SerializeField] private float _shotAngle = 45.0f;

    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _projectileLifetime = 0.5f;
    [SerializeField] private float _forwardOffset = 3.0f;

    public void Start() {
        _pool = new Pool<BasePlayerBomberProjectile>();
        _pool.MakePool(_prefab, QUEUE_MAX);
    }

    public void Fire() {
        Transform playerPosition = _player.GetTransform();
        Vector3 forward = _player.GetForward();
        
        PullTrigger(playerPosition.position + forward * _forwardOffset + Vector3.up, forward, _initialVelocity, (int)(_baseDamage * _player.GetAttackFactor()), (int)(_explosiveDamage * _player.GetAttackFactor()));
    }

    private void PullTrigger(in Vector3 currentPosition, in Vector3 forward, float initialVelocity, int mainDamage, int subDamage) {
        Vector3 left = new Vector3(currentPosition.x + -forward.z, currentPosition.y, currentPosition.z + forward.x);
        Vector3 addend = currentPosition - left;
        Vector3 rightDiagonal = Quaternion.AngleAxis(_shotAngle, Vector3.up) * forward;
        Vector3 leftDiagonal = Quaternion.AngleAxis(-_shotAngle, Vector3.up) * forward;

        BasePlayerBomberProjectile leftInstance = _pool.GetPooledObject();
        leftInstance.Fire(left, forward, left + leftDiagonal * _distance, leftDiagonal * initialVelocity, mainDamage, subDamage, _layer, _projectileLifetime);

        BasePlayerBomberProjectile centerInstance = _pool.GetPooledObject();
        centerInstance.Fire(currentPosition, forward, currentPosition + forward * _distance, forward * initialVelocity, mainDamage, subDamage, _layer, _projectileLifetime);

        BasePlayerBomberProjectile rightInstance = _pool.GetPooledObject();
        rightInstance.Fire(currentPosition + addend, forward, currentPosition + addend + rightDiagonal * _distance, rightDiagonal * initialVelocity, mainDamage, subDamage, _layer, _projectileLifetime);
    }
}