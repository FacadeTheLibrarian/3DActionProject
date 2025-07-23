using System;
using UniRx;
using System.Collections.Generic;
using UnityEngine;

internal sealed class MonsterHandler {

    private readonly List<BasePlayableMonster> MONSTERS = new List<BasePlayableMonster>();

    public BasePlayableMonster this[int index] {
        get { return MONSTERS[index]; }
    }

    public void Increment(int index) {
        if (index < 0 || index >= MONSTERS.Count) {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }
#if UNITY_EDITOR
#endif
        MONSTERS[index].gameObject.SetActive(false);
        MONSTERS[index + 1].gameObject.SetActive(true);
    }

    public MonsterHandler(in MonsterData monsterData, in Transform parent, in PlayerGeneration generation) {
        for(int i = 0; i < (int)PlayerGeneration.e_generation.max; i++) {
            BasePlayableMonster monster = monsterData.SummonMonster(i, parent);
            if(i != (int)generation.GetCurrentGeneration) {
                monster.gameObject.SetActive(false);
            }
            MONSTERS.Add(monster);
        }
    }
}