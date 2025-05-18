using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSetData", menuName = "ScriptableObjects/Monsters")]
internal sealed class MonsterSetData : ScriptableObject {
    [SerializeField] private BasePlayableMonster[] _monsterData = new BasePlayableMonster[(int)MonsterHandler.e_generation.max];
    public BasePlayableMonster this[int index] {
        get {
            return _monsterData[index];
        }
    }

    public BasePlayableMonster SummonMonster(int index, in Transform parent) {
        BasePlayableMonster instance = Instantiate(_monsterData[index], parent);
        return instance;
    }
}
