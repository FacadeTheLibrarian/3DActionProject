using System.Collections.Generic;
using UnityEngine;

internal interface IPlayableCharacter {
	public Animator GetAnimator();

	public void SetSpeed(float speed);
	public void NormalAttack();
	public void SpecialAttack();
}