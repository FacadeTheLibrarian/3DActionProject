using System;
using UnityEngine;

internal abstract class BaseEnemy : MonoBehaviour, IDisposable {
    public static event Action<float> OnDefeated;

    [SerializeField] private float _baseExp = 10;
    [SerializeField] protected int _hp = 20;

    [SerializeField] protected Animator _animator = default;

    protected IPlayerState _player;
    protected float _time = 0.0f;
    protected float _timeToReviseBehaviour = 1.0f;

    public void EnemyInitialization(in IPlayerState player) {
        _player = player;
    }

    public void EnemyUpdate() {
        _time += Time.deltaTime;
        if(_time >= _timeToReviseBehaviour) {
            ReviseBehaviour();
            _time = 0.0f;
        }
        InnerEnemyUpdate();
    }

    protected abstract void InnerEnemyUpdate();
    protected abstract void ReviseBehaviour();

    public void Dispose() {
        _animator = null;
    }

    protected void Defeated() {
        OnDefeated?.Invoke(_baseExp);
    }
}