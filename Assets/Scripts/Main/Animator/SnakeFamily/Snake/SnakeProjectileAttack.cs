using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeProjectileAttack : MonoBehaviour {
    private const int QUEUE_MAX = 4;

    [SerializeField] private Queue<BasePlayerCruisingProjectile> _pool = default;
    [SerializeField] private BasePlayerCruisingProjectile _prefab = default;

    [SerializeField] private PlayerGodClass _player;

    [SerializeField, Range(0, 100)] private int _baseDamage = 5;
    [SerializeField, Range(0, 100)] private int _explosiveDamage = 10;
    [SerializeField] private float _initialVelocity = 30.0f;
    [SerializeField] private float _forwardOffset = 2.0f;

    [SerializeField] private LayerMask _layer = default;

    public void Awake() {
        _pool = new Queue<BasePlayerCruisingProjectile>(QUEUE_MAX);
        for (int i = 0; i < QUEUE_MAX; i++) {
            BasePlayerCruisingProjectile instance = Instantiate(_prefab);
            _pool.Enqueue(instance);
        }
    }
    public void Fire() {
        Transform playerPosition = _player.GetTransform();
        Vector3 forward = _player.GetForward();
        PullTrigger(playerPosition.position + forward * _forwardOffset + Vector3.up, forward, _initialVelocity, (int)(_baseDamage * _player.GetAttackFactor()), (int)(_explosiveDamage * _player.GetAttackFactor()));
    }

    private void PullTrigger(in Vector3 currentPosition, in Vector3 forward, in float initialVelocity, int mainDamage, int subDamage) {
        BasePlayerCruisingProjectile instance = GetPooledObject();
        instance.Fire(currentPosition, forward, initialVelocity, mainDamage, subDamage, _layer);
    }

    private BasePlayerCruisingProjectile GetPooledObject() {
        if (_pool.Peek().GetIsOccupied()) {
            BasePlayerCruisingProjectile generatedInstance = Instantiate(_prefab);
            _pool.Enqueue(generatedInstance);
            return generatedInstance;
        }
        BasePlayerCruisingProjectile instance = _pool.Dequeue();
        _pool.Enqueue(instance);
        return instance;
    }
}