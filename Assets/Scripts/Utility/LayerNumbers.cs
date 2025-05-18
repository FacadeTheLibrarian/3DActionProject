using System.Collections.Generic;
using UnityEngine;

internal sealed class LayerNumbers {
	public enum e_layers : int{
        defaultLayer = 1,
        transparentFX = 2,
        ignoreRaycast = 4,
        player = 8,
        water = 16,
        UI = 32,
        staticTerrain = 64,
        enemy = 128,
    }
}