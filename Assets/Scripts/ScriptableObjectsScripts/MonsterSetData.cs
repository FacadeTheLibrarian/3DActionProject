using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSetData", menuName = "ScriptableObjects/Monsters")]
internal sealed class MonsterSetData : ScriptableObject {
    [SerializeField] private BasePlayableMonster[] _monsterData = new BasePlayableMonster[(int)BasePlayableMonster.e_generation.max];
    public BasePlayableMonster this[int index] {
        get {
            return _monsterData[index];
        }
    }
}
