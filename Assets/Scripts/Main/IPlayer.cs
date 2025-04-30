using System.Collections.Generic;
using UnityEngine;

internal interface IPlayer {
	public Vector3 GetPosition();
	public Quaternion GetRotation();
	public float GetHalfScaleY();
	public Vector3 GetForward();
}