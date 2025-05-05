using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeletProjectileAttack : BaseAttack {
    private const int QUEUE_MAX = 4;

    private Pool<PlayerCruisingProjectile> _pool = default;
    [SerializeField] private PlayerCruisingProjectile _prefab = default;

    [SerializeField, Range(0, 100)] private int _explosiveDamage = 5;
    [SerializeField] private float _initialVelocity = 30.0f;

    public void Start() {
        _pool = new Pool<PlayerCruisingProjectile>();
        _pool.MakePool(_prefab, QUEUE_MAX);
    }
    public void Fire() {
        Transform playerPosition = _player.GetTransform();
        Vector3 forward = _player.GetForward();
        PullTrigger(GetCastPosition(), forward, _initialVelocity, (int)(_baseDamage * _player.GetAttackFactor()), (int)(_explosiveDamage * _player.GetAttackFactor()));
    }

    private void PullTrigger(in Vector3 currentPosition, in Vector3 forward, in float initialVelocity, int mainDamage, int subDamage) {
        PlayerCruisingProjectile instance = _pool.GetPooledObject();
        instance.Fire(currentPosition, forward, initialVelocity, mainDamage, subDamage, _layer);
    }
}