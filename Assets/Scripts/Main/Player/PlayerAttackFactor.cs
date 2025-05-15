using System.Collections.Generic;
using UnityEngine;

internal sealed class PlayerAttackFactor : MonoBehaviour {
	private float _attackFactor = 1.0f;

    public float GetAttackFactor {
        get {
            return _attackFactor;
        }
    }
}