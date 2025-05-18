using UnityEngine;

internal sealed class NagaTripletFireBall : BaseAttack {
    private const int QUEUE_MAX = 12;

    private Pool<PlayerTimeBombProjectile> _pool = default;
    [SerializeField] private PlayerTimeBombProjectile _prefab = default;

    [SerializeField] private Vector3 _offset = new Vector3(1.0f, 0.0f, 0.0f);
    [SerializeField, Range(0, 100)] private int _explosiveDamage = 5;
    [SerializeField] private float _initialVelocity = 30.0f;

    [SerializeField] private float _shotAngle = 45.0f;

    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _projectileLifetime = 0.5f;

    protected override void InnerInitializatiton() {
        _pool = new Pool<PlayerTimeBombProjectile>();
        _pool.MakePool(_prefab, QUEUE_MAX);
    }

    public void Fire() {
        Transform playerPosition = _playerTransform ;
        Vector3 forward = _direction.GetCachedForward();
        int modifiedBaseDamage = (int)(_baseDamage * _attackFactor.GetAttackFactor);
        int modifiedExplosionDamage = (int)(_explosiveDamage * _attackFactor.GetAttackFactor);

        PullTrigger(GetInitialCastPosition(), forward, _initialVelocity, modifiedBaseDamage, modifiedExplosionDamage);
    }

    //FIXME: リファクタリング必須
    private void PullTrigger(in Vector3 currentPosition, in Vector3 forward, float initialVelocity, int mainDamage, int subDamage) {
        Vector3 modifiedOffset = Quaternion.LookRotation(forward, Vector3.up) * _offset;
        Vector3 rightDiagonal = Quaternion.AngleAxis(_shotAngle, Vector3.up) * forward;
        Vector3 leftDiagonal = Quaternion.AngleAxis(-_shotAngle, Vector3.up) * forward;

        PlayerTimeBombProjectile leftInstance = _pool.GetPooledObject();
        leftInstance.Fire(currentPosition, forward, currentPosition + (leftDiagonal * _distance), leftDiagonal * initialVelocity, mainDamage, subDamage, _layer, _projectileLifetime);

        PlayerTimeBombProjectile centerInstance = _pool.GetPooledObject();
        centerInstance.Fire(currentPosition + modifiedOffset, forward, currentPosition + (forward * _distance), forward * initialVelocity, mainDamage, subDamage, _layer, _projectileLifetime);

        PlayerTimeBombProjectile rightInstance = _pool.GetPooledObject();
        rightInstance.Fire(currentPosition + (modifiedOffset * 2.0f), forward, currentPosition + (rightDiagonal * _distance), rightDiagonal * initialVelocity, mainDamage, subDamage, _layer, _projectileLifetime);
    }
}