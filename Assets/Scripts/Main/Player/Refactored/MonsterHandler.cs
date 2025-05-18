using System;
using UniRx;
using System.Collections.Generic;
using UnityEngine;

internal sealed class MonsterHandler : IDisposable {
    public enum e_generation : int {
        first = 0,
        second = 1,
        third = 2,
        max,
    }
    private readonly List<BasePlayableMonster> MONSTERS = new List<BasePlayableMonster>();

    private readonly ReactiveProperty<MonsterHandler.e_generation> GENERATION = new ReactiveProperty<MonsterHandler.e_generation>(MonsterHandler.e_generation.third);
    public IReadOnlyReactiveProperty<MonsterHandler.e_generation> CurrentGeneration => GENERATION;
    public BasePlayableMonster this[int index] {
        get { return MONSTERS[index]; }
    }

    public void Dispose() {
        GENERATION.Dispose();
    }

    public MonsterHandler(in MonsterSetData monsterData, in Transform parent) {
        for(int i = 0; i < (int)MonsterHandler.e_generation.max; i++) {
            BasePlayableMonster monster = monsterData.SummonMonster(i, parent);
            MONSTERS.Add(monster);
        }
    }
}