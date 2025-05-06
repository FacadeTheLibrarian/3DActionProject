using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSetData", menuName = "ScriptableObjects/Monsters")]
internal sealed class MonsterSetData : ScriptableObject {
    [SerializeField] private GameObject _firstGenerationMonster = default;
    [SerializeField] private GameObject _secondGenerationMonster = default;
    [SerializeField] private GameObject _thirdGenerationMonster = default;

    [SerializeField] private PlayerMoveVariables _moveVariables = default;
    [SerializeField] private PlayerStaminaVariables _staminaVariables = default;
    [SerializeField] private PlayerAttackVariables _attackVariables = default;

    [SerializeField] private int _requiredExpToSecond = 0;
    [SerializeField] private int _requiredExpToThird = 0;

    public GameObject GetFirstGenerationMonster => _firstGenerationMonster;
    public GameObject GetSecondGenerationMonster => _secondGenerationMonster;
    public GameObject GetThirdGenerationMonster => _thirdGenerationMonster;
}
