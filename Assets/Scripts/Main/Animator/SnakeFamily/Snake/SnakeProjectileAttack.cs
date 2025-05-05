using SimpleMan.VisualRaycast;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SnakeProjectileAttack : MonoBehaviour {
    private const int QUEUE_MAX = 4;

    [SerializeField] private Queue<PlayerCruisingProjectile> _pool = default;
    [SerializeField] private PlayerCruisingProjectile _prefab = default;

    [SerializeField] private PlayerGodClass _player;

    [SerializeField, Range(0, 100)] private int _baseDamage = 5;
    [SerializeField, Range(0, 100)] private int _explosiveDamage = 10;
    [SerializeField] private float _initialVelocity = 30.0f;
    [SerializeField] private float _forwardOffset = 2.0f;

    [SerializeField] private LayerMask _layer = default;

    public void Awake() {
        _pool = new Queue<PlayerCruisingProjectile>(QUEUE_MAX);
        for (int i = 0; i < QUEUE_MAX; i++) {
            PlayerCruisingProjectile instance = Instantiate(_prefab);
            _pool.Enqueue(instance);
        }
    }
    public void Fire() {
        Transform playerPosition = _player.GetTransform();
        Vector3 forward = _player.GetForward();
        PullTrigger(playerPosition.position + forward * _forwardOffset + Vector3.up, forward, _initialVelocity, (int)(_baseDamage * _player.GetAttackFactor()), (int)(_explosiveDamage * _player.GetAttackFactor()));
    }

    private void PullTrigger(in Vector3 currentPosition, in Vector3 forward, in float initialVelocity, int mainDamage, int subDamage) {
        PlayerCruisingProjectile instance = GetPooledObject();
        instance.Fire(currentPosition, forward, initialVelocity, mainDamage, subDamage, _layer);
    }

    private PlayerCruisingProjectile GetPooledObject() {
        if (_pool.Peek().GetIsOccupied()) {
            PlayerCruisingProjectile generatedInstance = Instantiate(_prefab);
            _pool.Enqueue(generatedInstance);
            return generatedInstance;
        }
        PlayerCruisingProjectile instance = _pool.Dequeue();
        _pool.Enqueue(instance);
        return instance;
    }
}