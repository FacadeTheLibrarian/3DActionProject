using System.Collections.Generic;
using UnityEngine;

internal interface IPlayer {
	public Transform GetTransform();
	public float GetScaleY();
	public Vector3 GetForward();
	public float GetAttackFactor();
}