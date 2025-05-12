using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAttackData", menuName = "ScriptableObjects/MonsterAttackData")]
public class MonsterAttackData : ScriptableObject {
    [SerializeField] private BasePlayableMonster _firstGeneration = default;
    [SerializeField] private BasePlayableMonster _secondGeneration = default;
    [SerializeField] private BasePlayableMonster _thirdGeneration = default;
}