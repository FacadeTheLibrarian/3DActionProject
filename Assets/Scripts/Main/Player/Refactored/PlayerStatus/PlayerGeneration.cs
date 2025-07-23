using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerGeneration {
    public enum e_generation : int {
        first = 0,
        second = 1,
        third = 2,
        max,
    }
    private e_generation _currentGeneration = e_generation.first;
    public e_generation GetCurrentGeneration => _currentGeneration;

    public void GrowthToNextGeneration() {
        if (_currentGeneration < e_generation.max - 1) {
            _currentGeneration++;
        }
    }
#if UNITY_EDITOR
    public e_generation DebugSetGeneration {
        set { _currentGeneration = value; }
    }
    public void DebugGrowth() {
        _currentGeneration =(e_generation)((int)(_currentGeneration + 1) % (int)e_generation.max);
    }
#endif
}