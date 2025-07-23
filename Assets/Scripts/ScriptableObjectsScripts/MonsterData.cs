using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/Monsters")]
internal sealed class MonsterData : ScriptableObject {
    [SerializeField] private BasePlayableMonster[] _monsterData = new BasePlayableMonster[(int)PlayerGeneration.e_generation.max];
    [SerializeField] private float _expPointNeedForFirstToSecondGrowth = 0.0f;
    [SerializeField] private float _expPointNeedForSecondToThirdGrowth = 0.0f;
    [SerializeField] private float _expPointNeedForGrowthAttack = 0.0f;
    [SerializeField] private float _initialStamina = 0.0f;
    [SerializeField] private float _staminaRecoveryAmountPerSecond = 0.0f;
    [SerializeField] private float _baseStaminaConsumptionOnDodge = 0.0f;
    public BasePlayableMonster this[int index] {
        get {
            return _monsterData[index];
        }
    }
    public float GetExpPointNeedForFirstToSecondGrowth => _expPointNeedForFirstToSecondGrowth;
    public float GetExpPointNeedForSecondToThirdGrowth => _expPointNeedForSecondToThirdGrowth;
    public float GetExpPointNeedForGrowthAttack => _expPointNeedForGrowthAttack;
    public float GetInitialStamina => _initialStamina;
    public float GetStaminaRecoveryAmountPerSecond => _staminaRecoveryAmountPerSecond;
    public float GetBaseStaminaConsumptionOnDodge => _baseStaminaConsumptionOnDodge;

    public BasePlayableMonster SummonMonster(int index, in Transform parent) {
        BasePlayableMonster instance = Instantiate(_monsterData[index], parent);
        return instance;
    }
}
