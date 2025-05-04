using System.Collections.Generic;
using UnityEngine;

internal interface IPlayer {
	public Transform GetTransform();
	public float GetHalfScaleY();
	public Vector3 GetForward();
}