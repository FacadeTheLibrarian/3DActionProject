using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
	public void GetHit(int damageAmount, in Vector3 playerForward);
	public Vector3 GetPosition();
}