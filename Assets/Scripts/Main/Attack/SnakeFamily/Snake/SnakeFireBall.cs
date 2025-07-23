using UnityEngine;

internal sealed class SnakeFireBall : BaseAttack {
    private const int QUEUE_MAX = 4;

    private Pool<PlayerCruisingProjectile> _pool = default;
    [SerializeField] private PlayerCruisingProjectile _prefab = default;

    [SerializeField, Range(0, 100)] private int _explosionDamage = 5;
    [SerializeField] private float _initialVelocity = 30.0f;

    protected override void InnerInitializatiton() {
        _pool = new Pool<PlayerCruisingProjectile>();
        _pool.MakePool(_prefab, QUEUE_MAX);
    }
    public void Fire() {
        _stamina.UseStamina(_baseStaminaConsumption);
        Transform playerPosition = _playerTransform;
        Vector3 forward = _direction.GetCachedForward();
        int modifiedBaseDamage = (int)(_baseDamage * _attackFactor.GetAttackFactor);
        int modifiedExplosionDamage = (int)(_explosionDamage * _attackFactor.GetAttackFactor);
        PlayerCruisingProjectile instance = _pool.GetPooledObject();
        instance.Fire(GetInitialCastPosition(), forward, _initialVelocity, modifiedBaseDamage, modifiedExplosionDamage, _layer);
    }
}