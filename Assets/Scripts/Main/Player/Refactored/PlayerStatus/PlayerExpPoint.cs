using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

internal sealed class PlayerExpPoint : MonoBehaviour {
    private PlayerGeneration _playerGeneration = default;

    private float[] _expPointsNeedFor = new float[(int)PlayerGeneration.e_generation.max];
    
    public event Action<float> OnGainExp = default;
    public event Action OnGrowthReady = default;

    [SerializeField] private float _expTotal = 0.0f;
    [SerializeField] private float _currentExp = 0.0f;
    private bool _isGrowthReady = false;
    public bool IsGrowthReady => _isGrowthReady;

    public void Initialization(in MonsterData monsterData, in PlayerGeneration playerGeneration) {
        _expPointsNeedFor[(int)PlayerGeneration.e_generation.first] = monsterData.GetExpPointNeedForFirstToSecondGrowth;
        _expPointsNeedFor[(int)PlayerGeneration.e_generation.second] = monsterData.GetExpPointNeedForSecondToThirdGrowth;
        _expPointsNeedFor[(int)PlayerGeneration.e_generation.third] = monsterData.GetExpPointNeedForGrowthAttack;
        _playerGeneration = playerGeneration;
        BaseEnemy.OnDefeated += GainExpPoint;
    }

    public void GainExpPoint(float amount) {
        float nextExpPoint = _currentExp + amount;
        float expNeeds = _expPointsNeedFor[(int)_playerGeneration.GetCurrentGeneration];
        if (nextExpPoint >= expNeeds) {
            OnGrowthReady?.Invoke();
        }
        _currentExp = nextExpPoint;
        OnGainExp?.Invoke(_currentExp / expNeeds);
        _expTotal += amount;
    }
}