using System;
using UniRx;
using System.Collections.Generic;
using UnityEngine;

internal sealed class MonsterHandler {

    private readonly List<BasePlayableMonster> MONSTERS = new List<BasePlayableMonster>();

    public BasePlayableMonster this[int index] {
        get { return MONSTERS[index]; }
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