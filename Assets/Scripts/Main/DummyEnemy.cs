using System.Collections.Generic;
using UnityEngine;

internal sealed class DummyEnemy : MonoBehaviour, IEnemy{

	[SerializeField] private Animator _animator = default;
	public void GetHit() {
		_animator.Play("Damage");
	}
}