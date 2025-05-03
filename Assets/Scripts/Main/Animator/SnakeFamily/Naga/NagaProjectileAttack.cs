using System.Collections.Generic;
using UnityEngine;

internal sealed class NagaProjectileAttack : MonoBehaviour {
    private const int QUEUE_MAX = 12;
    private const int FIRE_ITERATION = 3;

    [SerializeField] private Queue<BasePlayerBomberProjectile> _pool = default;
    [SerializeField] private BasePlayerBomberProjectile _prefab = default;

    [SerializeField] private PlayerGodClass _player;
    private float _shotAngle = 45.0f;
    private float _distance = 5.0f;

    [SerializeField] private LayerMask _layer = default;

    public void Awake() {
        _pool = new Queue<BasePlayerBomberProjectile>(QUEUE_MAX);
        for (int i = 0; i < QUEUE_MAX; i++) {
            BasePlayerBomberProjectile instance = Instantiate(_prefab);
            _pool.Enqueue(instance);
        }
    }
    public void Fire() {
        Transform playerPosition = _player.GetTransform();
        Vector3 forward = _player.GetForward();
        PullTrigger(playerPosition.position + forward * 3.0f + Vector3.up, forward, 30, 10, 30);
    }

    private void PullTrigger(in Vector3 currentPosition, in Vector3 forward, float initialVelocity, int mainDamage, int subDamage) {
        Vector3 left = new Vector3(currentPosition.x + -forward.z, currentPosition.y, currentPosition.z + forward.x);
        Vector3 addend = currentPosition - left;
        Vector3 up = Vector3.up * 20.0f;
        Vector3 rightDiagonal = Quaternion.AngleAxis(_shotAngle, Vector3.up) * forward;
        Vector3 leftDiagonal = Quaternion.AngleAxis(-_shotAngle, Vector3.up) * forward;

        BasePlayerBomberProjectile leftInstance = GetPooledObject();
        leftInstance.Fire(left, forward, left + leftDiagonal * _distance, leftDiagonal * initialVelocity, mainDamage, subDamage, _layer, 0.5f);

        BasePlayerBomberProjectile centerInstance = GetPooledObject();
        centerInstance.Fire(currentPosition, forward, currentPosition + forward * _distance, forward * initialVelocity, mainDamage, subDamage, _layer, 0.5f);

        BasePlayerBomberProjectile rightInstance = GetPooledObject();
        rightInstance.Fire(currentPosition + addend, forward, currentPosition + addend + rightDiagonal * _distance, rightDiagonal * initialVelocity, mainDamage, subDamage, _layer, 0.5f);
    }

    private BasePlayerBomberProjectile GetPooledObject() {
        if (_pool.Peek().GetIsOccupied()) {
            BasePlayerBomberProjectile generatedInstance = Instantiate(_prefab);
            _pool.Enqueue(generatedInstance);
            return generatedInstance;
        }
        BasePlayerBomberProjectile instance = _pool.Dequeue();
        _pool.Enqueue(instance);
        return instance;
    }
}