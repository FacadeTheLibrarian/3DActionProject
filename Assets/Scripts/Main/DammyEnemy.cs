using System.Collections.Generic;
using UnityEngine;

internal sealed class DammyEnemy : MonoBehaviour, IEnemy{

	[SerializeField] private Animator _animator = default;
	public void GetHit() {
		_animator.Play("Damage");
	}
}